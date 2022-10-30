using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TestWPF.Commands;
using TestWPF.Models;
using TestWPF.Repositories;
using TestWPF.ViewModels.Base;

namespace TestWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Статистика пройденных шагов";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value); 
        }

        private string _status = "Готово!";

        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        private List<User> _users;
        public List<User> Users
        {
            get => _userRepository.All;
            set => Set(ref _users, value);
        }

        private string _dataPath;

        public string DataPath
        {
            get => _dataPath;
            set => Set(ref _dataPath, value);
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                Set(ref _selectedUser, value);
                var dataPoint = new List<DataPoint>();
                foreach (var info in _selectedUser.DaySteps)
                {
                    dataPoint.Add(new DataPoint() { X = info.Key, Y = info.Value.Steps});
                }
                DataPoints = dataPoint;
            }
        }

        private IEnumerable<DataPoint> _dataPoints;

        public IEnumerable<DataPoint> DataPoints
        {
            get => _dataPoints;
            set => Set(ref _dataPoints, value);
        }

        public ICommand SaveModelInFileCommand { get; }

        private bool CanSaveModelInFileExecuted(object p) => p is User user && Users.Contains(user);
        private void OnSaveModelInFileExecuted(object p)
        {
            MessageBoxResult result;
            foreach (User user in _savedUsers.All) {
                if (SelectedUser.Name.Equals(user.Name)) {
                    result = MessageBox.Show("Данная запись уже существует!");
                    return;
                }
            }
            if (_savedUsers.Save(SelectedUser))
            {
                result = MessageBox.Show("Запись успешно добавлена!");
            }
            else
            {
                result = MessageBox.Show("Ошибка!");
            }
        }
        

        private DayResultRepository _repository;
        private UserRepository _userRepository;
        private SavedUsers _savedUsers;

        public MainWindowViewModel()
        {
            _repository = new DayResultRepository();
            _userRepository = new UserRepository(_repository);
            _savedUsers = new SavedUsers();
            SaveModelInFileCommand = new Command(OnSaveModelInFileExecuted, CanSaveModelInFileExecuted);
        }
    }
}
