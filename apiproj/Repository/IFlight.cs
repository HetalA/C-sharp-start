using apiproj.Models;
namespace apiproj.Repository
{
    public interface IFlight<HetalFlight>
    {
        List<HetalFlight> ShowFlights();
        void AddFlight(HetalFlight flight);
        void EditFlight(int id, HetalFlight flight);
        HetalFlight GetFlightById(int id);
        void DeleteFlight(int id);
    }
}