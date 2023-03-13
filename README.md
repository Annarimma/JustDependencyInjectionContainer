# Just Dependency Injection Container

## :star: About repo
![Project Status](https://img.shields.io/badge/Status-In%20progress-blue) ![.NET Version](https://img.shields.io/badge/.NET-6.0-%23%09%2300cc66) ![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/Annarimma/JustDependencyInjectionContainer/dotnet.yml) ![GitHub issues](https://img.shields.io/github/issues/Annarimma/JustDependencyInjectionContainer)

## :star: Features
- [x] Managing life time instances.
- [x] Register [AddSingleton](#1-addsingleton) life time.
- [x] Register [AddTransient](#2-addtransient) life time.
- [x] Register [AddScoped](#3-addscoped) life time.
- [x] Simple registration with transient life time by default: ```Register<Service>()```
- [x] [As](#5-as) method support.
- [x] A lot of convenient interfaces.
- [x] Resolving dependencies.
- [x] Creating instances with reflection and lambda expressions.
- [x] Enable using construction.

Also:
- [x] Tests.
- [x] Benchmark.
- [x] Samples.
- [x] Exceptions and messages about errors while a container is building.

## :star:  How to use

### Sample 1
```csharp
// Create your builder.
var instance = new ContainerBuilder()
    // Register your dependencies.
    .AddTransient<IService, Service>()
    // Build your container.
    .Build()
    // Create a new Scope.
    .CreateScope()
    // Resolve IService and create a instance.
    .Resolve<IService>();
```
- - -
### Sample 2
```csharp
// Create your builder.
var ICInstance = new ContainerBuilder()
    // Register your Service as transient by default.
    .Register<CD>()
    // Register your IService type.
    .As<IC>()
    // Build your container.
    .Build()
    // Create a new Scope.
    .CreateScope()
    // Resolve IService and create a instance.
    .Resolve<IC>();
```

## Documentation

### :star: ContainerBuilder()

Instance to be created through reflection by default.
```csharp
// Create your builder.
var builder = new ContainerBuilder();
```

You can pass the next args to Container builder to customise building process:
```csharp
// Create your builder. ReflectionActivationBuilder() is be using as default.
var reflectiveBuilder = new ContainerBuilder(new ReflectionActivationBuilder());
// You also can use the equivalent: 
var builder = new ContainerBuilder();

// Create your builder with LambdaActivationBuilder.
var lambdaBuilder = new ContainerBuilder(new LambdaActivationBuilder());
```
- - -
### :star: Registration

#### 1. AddSingleton
```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:
builder.AddSingleton(typeof(IService), typeof(Service));
builder.AddSingleton<IService>(typeof(Service));
builder.AddSingleton<IService, Service>();
```
- - -
#### 2. AddTransient
```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:
builder.AddTransient(typeof(IService), typeof(Service));
builder.AddTransient<IService>(typeof(Service));
builder.AddTransient<IService, Service>();
```
- - -
#### 3. AddScoped
```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:
builder.AddScoped(typeof(IService), typeof(Service));
builder.AddScoped<IService>(typeof(Service));
builder.AddScoped<IService, Service>();
```
- - -
#### 4. Register

- - -
#### 5. As

- - -
### :star: Build()

### :star: CreateScope() and using

### :star: Resolve()
Resolving a component is roughly equivalent to calling “new” to instantiate a class.


### :star: Handle disposing