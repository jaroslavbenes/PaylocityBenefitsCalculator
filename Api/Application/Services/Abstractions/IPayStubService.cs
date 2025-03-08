using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

public interface IPayStubService
{
    PayStub GetPayStub(Employee employee);
}