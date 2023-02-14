using DIContainer.Sample.Abstractions;
using DIContainer.Sample.Models;

IService service = new Service();
var controller = new Controller(service);
controller.Read();