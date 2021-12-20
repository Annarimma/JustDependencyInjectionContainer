# Done
- [x] created exceptions и const exception messages
- [x] no dependence on the order of registration
- [x] build() in containerBuilder  
- [x] reduced the use of getParameters()


- [x] container and containerBuilder are working
- [x] restored tests
- [x] renamed projects and distributed files the files into folders
- [x] created readme.md

# To do
1. - [x] dependency container - обдумать интерфейс
2. - [x] build() в билдере - он должен вернуть рабочий di container - новый код
3. - [x] переписать тесты

## *
4. - [ ] два режима: первый - пре-компайл депенденси (тру/фолс)
5. - [ ] пре компайл - проверить что все зависимости зарегистрированные мы можем их инстациировать
6. - [ ] без пре - контейнер сам работает, мы не компилируем
7. - [ ] ошибка: ты забыл зависимости при пре комайл на входе

## **
8. - [ ] каких зависимостей не хватает

## Previous tasks
1. - [ ] подумать над как AddScope - именно на этот блок 
2. - [x] сократить использование гет параметрс, один раз для каждого типа и избавиться при инстансиировании 
3. - [x] зависимы от порядка регистрации, надо сделать так, чтобы вне зависимости

## Other Notes
1. - [x] сделать конкретные проверки, а не try-catch
2. - [x] подумать как сделать интерфейс более понятным, например:  
```
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IMyDependency, MyDependency>();

var app = builder.Build();
```
3. - [x] cached
4. - [ ] logger
5. - [ ] exception types
