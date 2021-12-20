# Dependency Injection Container

## Status
In development.

[![Build Status](https://app.travis-ci.com/Annarimma/JustDependencyInjectionContainer.svg?branch=main)](https://app.travis-ci.com/Annarimma/JustDependencyInjectionContainer)

## Functions
* register singleton scope;
* register transient scope;

## How to use

1. Instantiate the container:
```
var containerBuilder = new ContainerBuilder();
```
2. Register a service:
```
var registeredService = containerBuilder.AddSingleton<IService, Service>();
```
3. Take a service instance:
```
var generatedContainer = registeredService.Build();
var service = generatedContainer.GetInstance<IService>();
```

## TODOs
See TODO.md file.