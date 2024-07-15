# Неофициальный API клиент для сервиса "Мой Налог"
#### lknpd.nalog.ru

На данный момент реализован функционал для формирования чеков. 

Управление счетами не реализовано, но его не сложно добавить.
Для самостоятельного добавления новых запросов к API lknpd.nalog.ru оставлены открытыми меторы ```Get``` и ```Post``` в классе ```MoyNalog```

## Установка
Соберите проект самсотоятельно и подключите к проекту как .dll либо установите nuget пакет ```MoyNalog```

## Настройка
```csharp
var client = new MoyNalog("inn", "passw")
```
где ```inn``` - Ваш ИНН
где ```passw``` - пароль от личного кабинета налоговой

для запуска тестов установите ```inn``` и ```passw``` в файле ```MoyNalog.Tests\Properties\launchSettings.json```

## Создание чека
```csharp
List<ServiceInfo> services = [new ServiceInfo {
    Amount = 100m, // 100 рублей
    Name = "Test", // Наименование товара/услуги
    Quantity = 1, // количество
}];
// Act
var receiptUuid = await client.AddIncome(new Client() { 
  IncomeType = IncomeType.FROM_INDIVIDUAL // деньги получены от физического лица
  }, 
  services,
  DateTime.UtcNow // дата операции
  );
```

```receiptUuid``` - уникальный идентификатор чека

## Получение ссылки на картинку чека
```csharp
var imageUrl = client.GetRecipietUrl(receiptUuid);
```

## Получение инфо о чеке

```csharp
var income = await client.GetRecipiet(receiptUuid);
```


## Отмена чека

```csharp
var cancelledIncome = await client.CancelIncome(receiptUuid, DateTime.UtcNow);
```

## Список чеков

```csharp
var incomesList = await client.GetIncomes(new GetIncomesRequest {
    From = DateTime.Now.AddYears(-1),
    To = DateTime.Now,
    Limit = 10,
    SortBy = IncomesSortBy.OperationTimeDesc
});
```

```GetIncomesRequest``` так же поддерживает фильтрацию по: 
- типу клиента - ```BuyerType```
- состоянию чека - ```ReceiptType```