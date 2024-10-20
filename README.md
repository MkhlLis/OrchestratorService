## **Оркерстратор.**
(ссылка на сервис Monitoring https://github.com/MkhlLis/MonitoringService/tree/master)

Реализация хранилища InMemory (InMemoryStore.cs)
```
private readonly List<CustomerInfo> _customers = new();
private readonly List<Order> _orders = new();
```


Интерфейсы связаны с реализациями посредством DI (Startup.cs).
```
services.AddSingleton<IStore, InMemoryStore>();
services.AddHttpClient<IOrchestratorHandler, OrchestratorHandler>();
services.AddScoped<IOrchestratorHandler, OrchestratorHandler>();
services.AddSingleton<IProduceEventService, ProduceEventService>();
services.AddSingleton<IEventMapper<BookingRequest, BookingRequestEvent>, EventMapper>();
```

**1. Контроллер OrchestratorController**
API Реализует
1. POST /orchestrator/add-new-order -- функционал создания заказа. 
2. GET /orchestrator/get-all-orders -- вывод списка заказов клиентов.
3. GET /orchestrator/request-to-monitoring -- запрос в микросервисы мониторинга (передаем список заявок на 
   бронирование товара)
4. POST /orchestrator/order-event -- получаем от сервиса мониторинга информацию о том что товар из запроса
   бронирования был забронирован на пользователя (для перестроения очереди клиентов -- клиент на которого был
   забронирован товар встает последним в очереди). После перестроения очереди оркестратор повторно отправляет в
   сервис мониторинга обновлённый список заказов и очередей клиентов.


![Recording.gif](Recording.gif)

TODO: Реализация шаблонная, не претендует на готовую. CircuitBreaker, да и много еще чего. 
