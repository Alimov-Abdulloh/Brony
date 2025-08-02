using Brony.Domain;
using Brony.Models;
using Brony.Models.Stadiums;

namespace Brony.Services.Stadiums;

public interface IStadiumService
{
    void Create(StadiumCreateModel stadiumCreateModel);
    void Update(int id, StadiumUpdateModel stadiumUpdateModel);
    void Delete(int id);
    StadiumViewModel Get(int id);
    List<StadiumViewModel> GetAll(
        string search,
        decimal? price,
        DateTime? startTime,
        DateTime? endTime);
}
