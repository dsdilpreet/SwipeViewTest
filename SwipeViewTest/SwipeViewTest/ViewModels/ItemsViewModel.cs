using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SwipeViewTest.Models;
using SwipeViewTest.Views;
using System.Threading;

namespace SwipeViewTest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        CancellationTokenSource tokenSource;
        CancellationToken token;
        Task task;

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }
        public Command LoadItemsCommand { get; set; }
        public Command StartTaskCommand { get; set; }
        public Command<Item> MakeRetryInvisibleCommand { get; set; }
        public Command<Item> MakeRetryVisibleCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            StartTaskCommand = new Command(async () => await StartTask());
            MakeRetryInvisibleCommand = new Command<Item>((item) => MakeRetryInvisible(item));
            MakeRetryVisibleCommand = new Command<Item>((item) => MakeRetryVisible(item));

            StartTaskCommand.Execute(null);
            LoadItemsCommand.Execute(null);
        }

        // making the binding visible here does not cause a crash if RetryAvailable was made false by MakeRetryInvisible() before
        private void MakeRetryVisible(Item item)
        {
            item.RetryAvailable = true;
        }

        private void MakeRetryInvisible(Item item)
        {
            item.RetryAvailable = false;
        }

        private async Task StartTask()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            await Task.Delay(1000);

            Action action = new Action(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        // making the binding visible here cuses crash if RetryAvailable was made false by MakeRetryInvisible() before
                        items[i].RetryAvailable = true;
                        await Task.Delay(10);
                    }
                }
            });

            task = Task.Run(action, token);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}