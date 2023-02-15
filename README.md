# Dependency Injection Container

## About repo
![Project Status](https://img.shields.io/badge/Status-In%20progress-blue) ![.NET Version](https://img.shields.io/badge/.NET-6.0-%23%09%2300cc66) ![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/Annarimma/JustDependencyInjectionContainer/dotnet.yml) ![GitHub issues](https://img.shields.io/github/issues/Annarimma/JustDependencyInjectionContainer)

## Functions
```
AddSingleton<IService, Service>()
AddTransient<IService, Service>()
AddScope<IService, Service>()
```
* register transient scope;

## How to use

Samples folder demonstrates examples.

## What is it?
From Microsoft Documentation:

> Dependency injection addresses these problems through:
>
> 1. The use of an interface or base class to abstract the dependency implementation.
>
> 2. Registration of the dependency in a service container. .NET provides a built-in servicecontainer, [IServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider). Services are typically registered at the app's start-up and appended to an [IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection). Once all services are added, you use [BuildServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectioncontainerbuilderextensions.buildserviceprovider) to create the service container.
>
> 3. Injection of the service into the constructor of the class where it's used. The framework takes on the responsibility of creating an instance of the dependency and disposing of it when it's no longer needed.