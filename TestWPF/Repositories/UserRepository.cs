using System.Collections.Generic;
using System.Linq;
using TestWPF.Models;

namespace TestWPF.Repositories
{
    internal class UserRepository
    {
        public List<User> All { get; private set; }
        private readonly DayResultRepository _dayResults;

        public UserRepository(DayResultRepository dayResults)
        {
            _dayResults = dayResults;
            Init();
        }

        private void Init()
        {

            List<User> users = new List<User>();
            foreach (var result in _dayResults.All)
            {
                foreach (var user in result.Value)
                {
                    bool isExist = false;

                    foreach (User item in users)
                    {            
                        int steps = user.Steps;
                        if (item.Name == user.User)
                        {
                            isExist= true;
                            item.Steps += steps;
                            item.DaySteps.Add(result.Key, user);
                            if (item.BestResult < steps)
                                item.BestResult = steps;
                            if (item.WorstResult > steps)
                                item.WorstResult = steps;
                            break;
                        }
                    }
                    if (isExist == false)
                    {
                        var daySteps = new Dictionary<int, DayResult>();
                        daySteps.Add(result.Key, user);
                        users.Add(new User() { Name = user.User, Steps = user.Steps, BestResult = user.Steps, WorstResult = user.Steps, DaySteps = daySteps});
                    }
                }
            }
            All = users;

            foreach(User user in All)
            {
                user.AvarageResult = user.Steps / user.DaySteps.Keys.Count();
            }

        }
    }
}
