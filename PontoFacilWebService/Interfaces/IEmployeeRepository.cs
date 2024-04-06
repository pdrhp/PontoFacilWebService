﻿using PontoFacilSharedData.Data.Dtos;
using PontoFacilSharedData.Models;

namespace PontoFacilWebService.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> NewEmployee(Employee employee);
}