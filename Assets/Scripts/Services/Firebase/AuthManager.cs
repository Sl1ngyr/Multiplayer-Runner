using System;
using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Services.Scene;
using TMPro;
using UI.AuthMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Firebase
{
    public class DatabaseManager : MonoBehaviour
    {
        [Header("Firebase")] 
        [SerializeField] private DependencyStatus _dependencyStatus;
        private FirebaseAuth _auth;
        private FirebaseUser _user;

        [Space] 
        [Header("Login page fields")] 
        [SerializeField] private TMP_InputField _emailLoginField;
        [SerializeField] private TMP_InputField _passwordLoginField;
        [SerializeField] private Button _loginButton;
        
        [Space] 
        [Header("Registration page fields")] 
        [SerializeField] private TMP_InputField _nameRegisterField;
        [SerializeField] private TMP_InputField _emailRegisterField;
        [SerializeField] private TMP_InputField _passwordRegisterField;
        [SerializeField] private TMP_InputField _confirmPasswordRegisterField;
        [SerializeField] private Button _registrationButton;
        
        [Space]
        [SerializeField] private ErrorPopUpHandler _errorPopUpHandler;
        [SerializeField] private int _gameSceneBuildIndex = 1;

        [Inject] private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            StartCoroutine(CheckAndFixDependenciesAsync());
        }

        private IEnumerator CheckAndFixDependenciesAsync()
        {
            var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
            
            yield return new WaitUntil(() => dependencyTask.IsCompleted);

            _dependencyStatus = dependencyTask.Result;
            
            if (_dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

                yield return new WaitForEndOfFrame();

                StartCoroutine(CheckForAutoLogin());
            }
            else
            {
                Debug.Log("Could not resolve all firebase dependencies: " + _dependencyStatus);
            }
        }
        
        private void InitializeFirebase()
        {
            _auth = FirebaseAuth.DefaultInstance;
            
            _auth.StateChanged += AuthStateChanged;
            
            AuthStateChanged(this, null);
        }

        private IEnumerator CheckForAutoLogin()
        {
            if (_user != null)
            {
                var reloadUserTask = _user.ReloadAsync();

                yield return new WaitUntil(() => reloadUserTask.IsCompleted);

                AutoLogin();
            }
        }

        private void AutoLogin()
        {
            if (_user != null)
            {
                //_sceneLoader.TransitionToSceneByIndex(_gameSceneBuildIndex);
            }
        }
        
        private void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null
                                                           && _auth.CurrentUser.IsValid();
                if (!signedIn && _user != null)
                {
                    Debug.Log("Signed out " + _user.UserId);
                }

                _user = _auth.CurrentUser;

                if (signedIn)
                {
                    Debug.Log("Signed in " + _user.UserId);
                }
            }
        }

        private void RegistrationButton()
        {
            StartCoroutine(Registration(_emailRegisterField.text,_passwordRegisterField.text,_nameRegisterField.text));
        }

        private void LoginButton()
        {
            StartCoroutine(Login(_emailLoginField.text,_passwordLoginField.text));
        }
        
        private IEnumerator Registration(string email, string password, string username)
        {
            var entryData = IsCorrectnessEntryData(email, password, username);
            
            if (entryData != null)
            {
                _errorPopUpHandler.SetUpErrorToPopUp(entryData);
                yield break;
            }
            
            var RegisterTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                _errorPopUpHandler.SetUpErrorToPopUp(GetErrorMessage(ErrorDetection(RegisterTask)));
            }
            else
            {
                _user = RegisterTask.Result.User;

                if (_user != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };

                    Task profileTask = _user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                    if (profileTask.Exception != null)
                    {
                        ErrorDetection(profileTask);

                        _errorPopUpHandler.SetUpErrorToPopUp("Username Set Failed!");
                    }
                    else
                    {
                        _sceneLoader.TransitionToSceneByIndex(_gameSceneBuildIndex);
                    }
                }
            }
        }

        private string IsCorrectnessEntryData(string email, string password, string username)
        {
            if (username == "")
            {
                return "User name is empty";
            }
            else if (email == "")
            {
                return "Email field is empty";
            }
            else if (password != _confirmPasswordRegisterField.text)
            {
                return "Password Does Not Match!";
            }

            return null;
        }
        
        private AuthError ErrorDetection(Task task)
        {
            Debug.LogWarning(message: $"Failed to register task with {task.Exception}");
                            
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            
            return errorCode;
        }
        
        private string GetErrorMessage(AuthError errorCode)
        {
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    return "Missing Email";
                case AuthError.MissingPassword:
                    return "Missing Password";
                case AuthError.WeakPassword:
                    Debug.Log("Weeak");
                    return "Weak Password";
                case AuthError.EmailAlreadyInUse:
                    return "Email Already In Use";
            }

            return null;
        }
    

        private IEnumerator Login(string email, string password)
        {
            var LoginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                _errorPopUpHandler.SetUpErrorToPopUp(GetErrorMessage(errorCode));
            }
            else
            {
                _user = LoginTask.Result.User;

                Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
                
                _sceneLoader.TransitionToSceneByIndex(_gameSceneBuildIndex);
            }
        }

        private void OnEnable()
        {
            _registrationButton.onClick.AddListener(RegistrationButton);
            _loginButton.onClick.AddListener(LoginButton);
        }

        private void OnDisable()
        {
            _registrationButton.onClick.RemoveListener(RegistrationButton);
            _loginButton.onClick.RemoveListener(LoginButton);
            
            _auth.StateChanged -= AuthStateChanged;
            _auth = null;
        }
        
    }
}