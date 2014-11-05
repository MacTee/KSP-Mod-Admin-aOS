using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core.Controller
{
    public abstract class BaseController<T_Controller, T_View>
        where T_View : IView
    {
        /// <summary>
        /// Singleton of the controller.
        /// </summary>
        protected static T_Controller mInstance;


        /// <summary>
        /// Gets the singleton of the controller.
        /// </summary>
        public static T_Controller Instance
        {
            get
            {
                if (mInstance == null)
                {
                    ConstructorInfo ci = typeof(T_Controller).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, 
                                                                             null, Type.EmptyTypes, null);

                    //if (ci != null)
                        mInstance = (T_Controller)ci.Invoke(null);
                }

                return mInstance;
            }
        }

        /// <summary>
        /// Gets the view of the controller.
        /// </summary>
        [DefaultValue(null)]
        public static T_View View { get; private set; }


        /// <summary>
        /// Initializes the controller and hooks it to its view.
        /// </summary>
        /// <param name="view">The view the controller should hook to.</param>
        public static void Init(T_View view)
        {
            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;

            View = view;
            MethodInfo mi = typeof(T_Controller).GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null) // && Instance != null)
                mi.Invoke(Instance, null);
        }

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public static void InvalidateView()
        {
            View.InvokeIfRequired(() => { View.InvalidateView(); });
        }

        /// <summary>
        /// Call this method to disable all controls on the UserControl.
        /// </summary>
        public static void AsyncTaskStarted(object sender)
        {
            //if (sender.GetType() == typeof(T_Controller) && sender.Equals(Instance))
            //    return;

            MethodInfo mi = typeof(T_Controller).GetMethod("AsyncroneTaskStarted", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(Instance, new object[] { Instance });
        }

        /// <summary>
        /// Call this method to enable all controls on the UserControl.
        /// </summary>
        public static void AsyncTaskDone(object sender)
        {
            //if (sender.GetType() == typeof(T_Controller) && sender.Equals(Instance))
            //    return;

            MethodInfo mi = typeof(T_Controller).GetMethod("AsyncroneTaskDone", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(Instance, new object[] { Instance });
        }

        /// <summary>
        /// Call this method when a language change has been occurred.
        /// </summary>
        public static void LanguageChanged(object sender)
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(GetLanguageManager(), View as Control, OptionsController.SelectedLanguage);

            //if (sender.GetType() == typeof(T_Controller) && sender.Equals(Instance))
            //    return;

            MethodInfo mi = typeof(T_Controller).GetMethod("LanguageHasChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi != null)
                mi.Invoke(Instance, new object[] { Instance });
        }

        /// <summary>
        /// Gets the LanguageDictionaryManager for this controller.
        /// Override this method to provide your own LanguageMangaer instance (if necessary).
        /// </summary>
        /// <returns>The LanguageDictionaryManager for this controller.</returns>
        public static Localizer GetLanguageManager()
        {
            return Localizer.GlobalInstance;
        }


        /// <summary>
        /// This method gets called when your Controller should be initialized.
        /// Perform additional initialization of your UserControl here.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// This method gets called when a critical asynchrony task will be started.
        /// Disable all controls of your View here to avoid multiple critical KSP MA changes.
        /// </summary>
        protected abstract void AsyncroneTaskStarted(object sender);

        /// <summary>
        /// This method gets called when a critical asynchrony task is complete.
        /// Enable the controls of your View again here.
        /// </summary>
        protected abstract void AsyncroneTaskDone(object sender);

        /// <summary>
        /// This method gets called when the language of KSP MA was changed.
        /// Perform extra translation work for your View here.
        /// </summary>
        protected abstract void LanguageHasChanged(object sender);
    }
}
