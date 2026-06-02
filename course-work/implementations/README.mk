Никола Христозов
2401321017

DiaManager (DiabetesAPI - ASP.NET CORE WEB API ; DiabetesBlazor - Blazor WebAssembly)
Система, предназначена за диабетици. 
Разполага със списък от храни с техните макронутриенти и гличемичен индекс. 
Позволява вписване на нива на инсулин и кръвна захар. 
Пресмята нужната доза инсулин, според това колко ще вдигне кръвната захар дадено ядене.

Инструкции за инсталиране:
1. Клонирате хранилището локално
2. Отваряте DiabetesAPI (`cd DiabetesAPI`) и изпълнявате `dotnet restore`
3. Променята "DefaultConnection" в appsettings.json, за да съвпада на вашият локален MSSQL Server.
4. Обновявате базата данни `dotnet ef database update`
5. Стартирате DiabetesAPI и DiabetesBlazor чрез `dotnet run`
