# Just Dependency Injection Container

## :star: About repo
![Project Status](https://img.shields.io/badge/Status-In%20progress-blue) ![.NET Version](https://img.shields.io/badge/.NET-6.0-%23%09%2300cc66) ![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/Annarimma/JustDependencyInjectionContainer/dotnet.yml) ![GitHub issues](https://img.shields.io/github/issues/Annarimma/JustDependencyInjectionContainer)

This README.md is my summary of the DI Container and a description of my project in one document.

## :star: Features
- [x] Creating instances of classes and manages their lifetime.
- [x] Resolving all instances from scope, not directly from container.
- [x] Register [Singletons](#1-singletons) life time.
- [x] Register [Transient](#2-addtransient) life time.
- [x] Register [Scoped](#3-addscoped) life time.
- [x] Simple registration with transient life time by default: ```Register<Service>()```
- [x] [As](#5-as) method support.
- [x] A lot of convenient interfaces.
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
var instance = new ContainerBuilder()
    // Register your Service as transient by default.
    .Register<Service>()
    // Register your IService type.
    .As<IService>()
    // Build your container.
    .Build()
    // Create a new Scope.
    .CreateScope()
    // Resolve IService and create a instance.
    .Resolve<IC>();
```
- - -
### Sample 3
```csharp
// Create your builder.
var instance = new ContainerBuilder()
    // Register your dependencies.
    .AddSingleton<IService>(new Service())
    // Build your container.
    .Build()
    // Create a new Scope.
    .CreateScope()
    // Resolve IService and create a instance.
    .Resolve<IService>();
```

## Documentation

### :star: ContainerBuilder()

Instance to be created through reflection by default.
```csharp
// Create your builder.
var builder = new ContainerBuilder();
```

You can pass the next args to Container builder to customize building process:
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

#### 1. Singletons

Just Dependency Container supports a Singleton lifetime. An instance is created the first time it is requested and remains for the lifetime of the container. 

```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:

// Type based registration
builder.AddSingleton(typeof(IService), typeof(Service));
builder.AddSingleton<IService>(typeof(Service));
builder.AddSingleton<IService, Service>();

// Lambda based registration
builder.AddSingleton(typeof(IRepository), s => new Repository());
builder.AddSingleton<IRepository>(s => new Repository());

// Instance based registration
builder.AddSingleton(typeof(IRepository), new Repository());
builder.AddSingleton<IRepository>(new Repository());
```

**Use Cases:**
- **Stateless classes** that provided functionality used other classes
- **Pooled resources**
- **In-memory queues**
- **Factory classes** - only need one instance as it is the methods that create instances of the required dependency.
- **Ambient Singletons** - such as the HttpContextAccessor that hide scope complexity by using AsyncLocal to attach the current Async context and allows injection into other singletons.
- **Heavily used lookup classes** such as **read-only caches** which do not change through the application lifetime once initialized.

- - -
#### 2. AddTransient

Just Dependency Container supports a Transient lifetime. It can create a new instance every time a request is made to the container.

```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:

// Type based registration
builder.AddTransient(typeof(IService), typeof(Service));
builder.AddTransient<IService>(typeof(Service));
builder.AddTransient<IService, Service>();

// Lambda based registration
builder.AddTransient(typeof(IService), s => new Service());
builder.AddTransient<IService>(s => new Service());
```
- - -
#### 3. AddScoped

A new instance is created for each unit of work, but acts like a singleton for the lifetime of that unit of work.

```csharp
// Context.
var builder = new ContainerBuilder();
```
```csharp
// You can use one of the options:

// Type based registration
builder.AddScoped(typeof(IService), typeof(Service));
builder.AddScoped<IService>(typeof(Service));
builder.AddScoped<IService, Service>();

// Lambda based registration
builder.AddScoped(typeof(IService), s => new Service());
builder.AddScoped<IService>(s => new Service());
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

### Scope Compatibility Notes (will be in the future)

| From ↓ Into → | Transient | Scoped | Singleton |
|---------------|-----------|--------|-----------|
|Transient      |    [x]    |    x   |     x     |
|Scoped         |    [x]    |   [x]  |     x     |
|Singleton      |    [x]    |   [x]  |    [x]    |

MSDI creates a 'Captured Dependency' where the shorter lifetime object is trapped for the lifetime of the longer lived object.

There is a exception InvalidOperationException: Cannot consume scoped services 'AScopedThing' from singleton 'ASingletonThing'. Happens when ValidateScopes is set to true in the ServiceProviderOptions when BuildServiceProvider is called.

Only happens by default if environment is development.
Can set manually for other environments, but there is a performance hit, so the advice is not to do it.

### Possible Problems
#### Singletons

A read-write singleton needs to be made thread-safe as no guarantees who will call it and how it will be called.

Give through to avoiding race condition:
- For collections, use the generic Concurrent collections as these will take care of thread-safety for you.
- For read-only properties, ensure that the underlying field is marked as Read Only in code and only set in the constructor / field initializer.
- For read-write properties - ensure that the code includes a thread lock so that only one thread at a time can make an update, or consider AsyncLocal storage for performance if there is no inter-dependencies.
- If the class implements IDisposable, ensure that this does not leak out to consumers via the container as a consumer could call Dispose() and stop all other consumers from working - consider registering as an interface that does not include Dispose instead of registering the class itself.

## Information Sources I used in this README
- [JetBrains .NET 5 Dependency Injection webinar](https://www.youtube.com/watch?v=0x2KW-dJDQU).
- [Autofac documentation.](https://autofac.readthedocs.io/en/latest/getting-started/index.html)

Thanks!