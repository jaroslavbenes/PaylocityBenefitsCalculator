using Api.Models;

namespace Api.Services.Abstractions;

public interface IPayStubService
{
    PayStub GetPayStub(Employee employee);
}