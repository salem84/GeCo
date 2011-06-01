/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace GeCo.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}

            CreateRicercaSostituto();
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearRicercaSostituto();    
        }


        #region RICERCA SOSTITUTO 
        private static RicercaSostitutoVM _viewModelRicercaSostituto;

        /// <summary>
        /// Gets the RicercaSostituto property.
        /// </summary>
        public static RicercaSostitutoVM RicercaSostitutoStatic
        {
            get
            {
                if (_viewModelRicercaSostituto == null)
                {
                    CreateRicercaSostituto();
                }

                return _viewModelRicercaSostituto;
            }
        }

        /// <summary>
        /// Gets the RicercaSostituto property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RicercaSostitutoVM RicercaSostituto
        {
            get
            {
                return RicercaSostitutoStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the RicercaSostituto property.
        /// </summary>
        public static void ClearRicercaSostituto()
        {
            _viewModelRicercaSostituto.Cleanup();
            _viewModelRicercaSostituto = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the RicercaSostituto property.
        /// </summary>
        public static void CreateRicercaSostituto()
        {
            if (_viewModelRicercaSostituto == null)
            {
                _viewModelRicercaSostituto = new RicercaSostitutoVM();
            }
        }
        #endregion

    }
}