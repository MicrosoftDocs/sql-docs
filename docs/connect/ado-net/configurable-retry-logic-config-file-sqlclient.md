---
title: Configurable retry logic configuration file with SqlClient
description: Learn how to use a configuration file to specify default retry logic providers and customize retry logic options in Microsoft.Data.SqlClient.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: 03/22/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configurable retry logic configuration file with SqlClient

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The default retry method when the safety switch is enabled is the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType> for both <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand>. You can specify a different retry method by using a configuration file.

## Configuration sections

Default retry logic options for an application can be changed by adding the following sections inside the `configSections` section of the configuration file:

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

- `AppContextSwitchOverrides`: .NET Framework supports AppContext switches via an [AppContextSwitchOverrides](/dotnet/framework/configure-apps/file-schema/runtime/appcontextswitchoverrides-element) section, which doesn't need to be defined explicitly. To turn on a switch in **.NET Core**, you must specify this section.

```csharp
<section name="AppContextSwitchOverrides"
        type="Microsoft.Data.SqlClient.AppContextSwitchOverridesSection, Microsoft.Data.SqlClient"/>
```

> [!NOTE]
> The following configurations should be specified inside the `configuration` section. Declare these new sections to configure the default retry logic through an application configuration file.

### Enable safety switch

> [!NOTE]
> Starting from Microsoft.Data.SqlClient v4.0, the App Context switch "Switch.Microsoft.Data.SqlClient.EnableRetryLogic" will no longer be required to use the configurable retry logic feature. The feature is now supported in production. The default behavior of the feature will continue to be a non-retry policy, which will need to be overridden by client applications to enable retries.

You can enable the safety switch through a configuration file. To learn how to enable it through application code, see [Enable configurable retry logic](appcontext-switches.md#enable-configurable-retry-logic).

- **.NET Framework**: For more information, see [AppContextSwitchOverrides element](/dotnet/framework/configure-apps/file-schema/runtime/appcontextswitchoverrides-element).

```csharp
<runtime>
    <AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableRetryLogic=true"/>
</runtime>
```

- **.NET Core**: supports multiple, semi-colon (;) delimited switches like .NET Framework.

```csharp
<AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableRetryLogic=true"/>
```

### Connection section

The following attributes can be used to specify the default retry logic for all <xref:Microsoft.Data.SqlClient.SqlConnection> instances in an application:

- **numberOfTries**: sets the number of times to try.

- **deltaTime**: sets the gap time interval as a <xref:System.TimeSpan> object.

- **minTime**: sets the allowed minimum gap time interval as a <xref:System.TimeSpan> object.

- **maxTime**: sets the allowed maximum gap time interval as a <xref:System.TimeSpan> object.

- **transientErrors**: sets the list of transient error numbers on which to retry.

- **retryMethod**: specifies a retry method creator that receives the retry configuration via a <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> parameter and returns a <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider> object.

- **retryLogicType**: sets a custom retry logic provider, which contains the retry method creators that provide the `retryMethod`. These methods should meet the criteria for `retryMethod`. The fully qualified type name of the provider should be used. For more information, see [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names).

> [!NOTE]
> It's not required to specify the `retryLogicType` if you use the built-in retry providers. To find the built-in retry providers, see [Internal retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md).

### Command section

The following attribute can also be set for all <xref:Microsoft.Data.SqlClient.SqlCommand> instances in an application:

- **authorizedSqlCondition**: Sets a pre-retry regular expression for <xref:Microsoft.Data.SqlClient.SqlCommand.CommandText%2A?displayProperty=nameWithType> to filter specific SQL statements.

> [!NOTE]
> The regular expression is case sensitive.

### Examples

- Attempts to establish a connection up to three times with an approximate 1-second delay between tries by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

    ```csharp
    <SqlConfigurableRetryLogicConnection retryMethod ="CreateFixedRetryProvider" 
                                            numberOfTries ="3" deltaTime ="00:00:01"/>
    ```

- Attempts to establish a connection up to five times with up to a 45-second delay between tries by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

    ```csharp
    <SqlConfigurableRetryLogicConnection retryMethod ="CreateExponentialRetryProvider" 
                        numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"/>
    ```

- Attempts to execute a command up to four times with a delay between 2 and 30 seconds by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

    ```csharp
    <SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider"
                        numberOfTries ="4" deltaTime ="00:00:02" maxTime ="00:00:30"/>
    ```

- Attempts to execute a command up to eight times with a delay from one second to one minute. It's limited to commands with `CommandText` containing the word `SELECT` and exception numbers 102 or 997. It uses the built-in <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType> method:

    ```csharp
    <SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider" 
                            numberOfTries ="8" deltaTime ="00:00:01" maxTime ="00:01:00"
                            transientErrors="102, 997"
                            authorizedSqlCondition="\b(SELECT)\b"/>
    ```

> [!NOTE]
> In the next two samples, you can find the custom retry logic source code from [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md#example). It's assumed the `CreateCustomProvider` method is defined in the `CustomCRL_Doc.CustomRetry` class in the `CustomCRL_Doc.dll` assembly that is in the application's executing directory.

- Attempts to establish a connection up to five times, with a delay between 3 and 45 seconds, error numbers 4060, 997, and 233 in the list, and using the specified custom retry provider:

    ```csharp
    <SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        retryMethod ="CreateCustomProvider" 
                        numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                        transientErrors ="4060, 997, 233"/>
    ```

- This sample behaves like the previous one:

    ```csharp
    <SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc"
                        retryMethod ="CreateCustomProvider" 
                        numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                        transientErrors ="4060, 997, 233"/>
    ```

> [!NOTE]
> Retry logic providers will be cached at the first use on a connection or command for future use during an application's lifetime.

> [!NOTE]
> Any errors when reading an application configuration file for retry logic settings won't cause errors in the application. The default <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType> will be used instead.
>
> You can use event source tracing to verify or troubleshoot issues with configuring retry logic. For more information, see [Enable event tracing in SqlClient](enable-eventsource-tracing.md).

## See also

- [Enable configurable retry logic](appcontext-switches.md#enable-configurable-retry-logic)
- [Internal retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md)
- [Enable event tracing in SqlClient](enable-eventsource-tracing.md)
- [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
