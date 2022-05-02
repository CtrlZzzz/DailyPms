using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DailyPmsShared;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public interface ITestWhenGettingAll<T> where T : class, IEntity
    {
        //HttpClient TestingClient { get; }
        IList<T> BuildTestItems();
        Task It_should_return_200_ok();
        Task It_should_return_all_items();
        Task It_should_return_the_correct_items();
    }
}

