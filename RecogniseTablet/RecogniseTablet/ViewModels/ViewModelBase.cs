using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IApplicationManager ApplicationManager { get; private set; }
        private readonly IPageDialogService _dialogService;
        private bool isProcessing = false;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, IApplicationManager applicationManager, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            this.ApplicationManager = applicationManager;
            _dialogService = dialogService;
        }

        public bool IsProcessing
        {
            get
            {
                return this.isProcessing;
            }
            set
            {
                this.SetProperty(ref this.isProcessing, value);
            }
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
