---
title: "Configurable retry logic and configuration file"
description: "Demonstrates how to specify default retry logic providers through a configuration file."
ms.date: "03/22/2021"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Configurable retry logic and configuration file

[!INCLUDE[appliesto-netfx-netcore-xxxx-md](../../includes/appliesto-netfx-netcore-xxxx-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The default retry method when the safety switch's enabled is <xref:Microsoft.Data.SqlClient.SqlConnection.SqlConfigurableRetryFactory.CreateNoneRetryProvider>, either for <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlCommand> unless you specify a specific retry method through the configuration file.

## Configuration sections

All the default retry logic configurations for an application can be specified after adding the following sections inside the `configSections` section at configuration file:

- `SqlConfigurableRetryLogicConnection`: to specify the default retry logic for <xref:Microsoft.Data.SqlClient.SqlConnection>.  

```csharp  
<section name="SqlConfigurableRetryLogicConnection"
        type="Microsoft.Data.SqlClient.SqlConfigurableRetryConnectionSection, Microsoft.Data.SqlClient"/>
```  

- `SqlConfigurableRetryLogicCommand`: to specify the default retry logic for <xref:Microsoft.Data.SqlClient.SqlCommand>.

```csharp  
<section name="SqlConfigurableRetryLogicCommand"
        type="Microsoft.Data.SqlClient.SqlConfigurableRetryCommandSection, Microsoft.Data.SqlClient"/>
```  

- `AppContextSwitchOverrides`: .NET Framework supports AppContext switches internally by `AppContextSwitchOverrides` section and doesn't need to be defined explicitly; but to turn on a switch in .NET Core you must specify this section.

```csharp  
<section name="AppContextSwitchOverrides"
        type="Microsoft.Data.SqlClient.AppContextSwitchOverridesSection, Microsoft.Data.SqlClient"/>
```  

> [!NOTE]  
> Following configurations should be specified inside the `configuration` section after declaring these new sections to set the default retry logic through an application configuration file.

### Enable safety switch

It's possible to turn on the considered safety switch through the configuration file. To find how to enable it through application code, see [Enable configurable retry logic](appcontext-switches.md#Enable-configurable-retry-logic).

- **.NET Framework**: For more information, see [AppContextSwitchOverrides element](/dotnet/framework/configure-apps/file-schema/runtime/appcontextswitchoverrides-element).

```csharp
<runtime>
    <AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableRetryLogic=true"/>
</runtime>
```

- **.NET Core**: supports more than one switch like .NET Framework.

```csharp
<AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableRetryLogic=true"/>
```

### Connection section

Following attributes can be used to specify the default retry logic for all <xref:Microsoft.Data.SqlClient.SqlConnection> instances in an application:  

- **numberOfTries**: sets the number of times to try. For more information, see <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.NumberOfTries>.

- **deltaTime**: sets the gap time interval as a <see cref="T:System.TimeSpan" /> object. For more information, see <xref:Microsoft.Data.SqlClient.DeltaTime.NumberOfTries>.

- **minTime**: sets the minimum allowed gap time interval as a <see cref="T:System.TimeSpan" /> object. For more information, see <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.MinTimeInterval>.

- **maxTime**: sets the allowed maximum gap time interval as a <see cref="T:System.TimeSpan" /> object. For more information, see <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.MaxTimeInterval>.

- **transientErrors**: sets the list of transient error numbers on which to retry when they occur. For more information, see <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.TransientErrors>.

- **retryMethod**: specifies a retry method creator that receives the retry configurations through a <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> parameter and returns a <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider> object.

- **retryLogicType**: sets custom retry logic `fully qualified type name`, which contains the retry method creators to specify by `retryMethod`. These methods should meet the required criteria for `retryMethod`. For more information, see [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names).

> [!NOTE]  
> It's not required to specify the `retryLogicType` if you use the internal pre-defined retry providers. To find the pre-defined retry providers, see [Internal retry logic providers](internal-retry-logic-providers.md).

### Command section

In addition to all the above attributes, the following attribute can be set to specify the default retry logic for all <xref:Microsoft.Data.SqlClient.SqlCommand> instances in an application:

- **authorizedSqlCondition**: Sets a pre-retry validation function over <see cref="T:Microsoft.Data.SqlClient.SqlCommand.CommandText"/>, only to include specific SQL statements. For more information, see <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption.AuthorizedSqlCondition>.

> [!NOTE]  
> Consider the regular expression is case sensitive.

### Examples

- Attempts to establish a connection maximum three times and about 1-second delay time before each retry by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider> method and default transient error list:  
```csharp
<SqlConfigurableRetryLogicConnection retryMethod ="CreateFixedRetryProvider" 
                                        numberOfTries ="3" deltaTime ="00:00:01"/>
```

- Attempts to establish a connection maximum five times and maximum 45-seconds delay time before each retry by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider> method and default transient error list:  
```csharp
<SqlConfigurableRetryLogicConnection retryMethod ="CreateExponentialRetryProvider" 
                    numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"/>
```

- Attempts to execute a command maximum four times and starts delay time from 2 seconds to maximum 30 seconds by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider> method and default transient error list:  
```csharp
<SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider"
                    numberOfTries ="4" deltaTime ="00:00:02" maxTime ="00:00:30"/>
```

- Attempts to execute a command start with `SELECT` statement for maximum eight times and starts delay time from 1 second to maximum 1 minute when exception numbers 102 or 997 happen by <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider> method:  
```csharp
<SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider" 
                        numberOfTries ="8" deltaTime ="00:00:01" maxTime ="00:01:00"
                        transientErrors="102, 997"
                        authorizedSqlCondition="\b(SELECT)\b"/>
```

> [!NOTE]  
> In the next samples, you can find the custom retry logic source code from [Core application program interfaces](core-application-program-interfaces.md#Example). It's assumed the `CreateCustomProvider` method is defined in `CustomCRL_Doc.CustomRetry` class by `CustomCRL_Doc.dll` assembly that must be copy to the application running directory.

- Attempts to establish a connection maximum 5 times and delay time before each execution from 3 seconds to 45 seconds in encounter with error numbers 4060, 997, and 233 by the specified customized retry provider:  
```csharp
<SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                    retryMethod ="CreateCustomProvider" 
                    numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                    transientErrors ="4060, 997, 233"/>
```

- This sample behaves like the previous:
```csharp
<SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc"
                    retryMethod ="CreateCustomProvider" 
                    numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                    transientErrors ="4060, 997, 233"/>
```

> [!NOTE]  
> Both default retry logics will be resolved and cached at the first attempt to open a connection or executing a command for further usage in an application lifetime.

> [!NOTE]  
> Any failures in the process of resolving the default retry logic methods through an application configuration file won't interrupt the application and will use the default <xref:Microsoft.Data.SqlClient.SqlConnection.SqlConfigurableRetryFactory.CreateNoneRetryProvider> instead of the specified settings.

> [!NOTE]  
> Use the event source tracing logs for more investigation on the process of resolving the default retry logic methods. For more information, see [Enable event tracing in SqlClient](enable-eventsource-tracing.md).

## See also

- [Enable configurable retry logic](appcontext-switches.md#Enable-configurable-retry-logic)
- [Internal retry logic providers](internal-retry-logic-providers.md)
- [Enable event tracing in SqlClient](enable-eventsource-tracing.md)
- [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names)
- [Configurable retry logic with SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
