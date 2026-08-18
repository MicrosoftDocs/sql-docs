---
title: Configure SqlClient Retry Logic with a Configuration File
description: Configure application-wide Microsoft.Data.SqlClient connection and command retry providers, timing, transient errors, and command filters.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Configure SqlClient retry logic with a configuration file

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Use an application configuration file to assign default retry providers to every <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlCommand> instance in the process. Without these sections or an object-level provider assignment, SqlClient uses <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType> and doesn't retry.

## Configuration sections

Declare the retry sections inside the `configSections` element. Section declarations must appear before the corresponding configuration values.

- `SqlConfigurableRetryLogicConnection`: Sets the default provider for <xref:Microsoft.Data.SqlClient.SqlConnection>.

```xml
<section name="SqlConfigurableRetryLogicConnection"
        type="Microsoft.Data.SqlClient.SqlConfigurableRetryConnectionSection, Microsoft.Data.SqlClient"/>
```

- `SqlConfigurableRetryLogicCommand`: Sets the default provider for <xref:Microsoft.Data.SqlClient.SqlCommand>.

```xml
<section name="SqlConfigurableRetryLogicCommand"
        type="Microsoft.Data.SqlClient.SqlConfigurableRetryCommandSection, Microsoft.Data.SqlClient"/>
```

### Connection section

Set the default retry logic for all <xref:Microsoft.Data.SqlClient.SqlConnection> instances in the application by using the following attributes:

- **numberOfTries**: Sets the total number of attempts, including the initial operation. The valid range is 1 through 60.

- **deltaTime**: Sets the gap time interval as a <xref:System.TimeSpan> object.

- **minTime**: Sets the allowed minimum gap time interval as a <xref:System.TimeSpan> object.

- **maxTime**: Sets the allowed maximum gap time interval as a <xref:System.TimeSpan> object.

- **transientErrors**: Sets a comma-separated list of error numbers to retry. If you omit it, the provider uses the [built-in transient error list](internal-retry-logic-providers-sqlclient.md#built-in-transient-error-list). If you specify it, your list replaces the built-in list.

- **retryMethod**: Specifies a retry method creator that receives the retry configuration through a <xref:Microsoft.Data.SqlClient.SqlRetryLogicOption> parameter and returns a <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider> object.

- **retryLogicType**: Sets a custom retry logic provider that contains the retry method creators identified by `retryMethod`. Those methods must meet the criteria for `retryMethod`. Use the fully qualified type name of the provider. For more information, see [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names).

> [!NOTE]  
> You don't need to specify `retryLogicType` when you use a built-in provider. For the available methods, see [Built-in retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md).

### Command section

Set the default retry logic for all <xref:Microsoft.Data.SqlClient.SqlCommand> instances in the application by using the connection-section attributes and the following command-specific attribute:

- **authorizedSqlCondition**: Sets a regular expression that <xref:Microsoft.Data.SqlClient.SqlCommand.CommandText%2A?displayProperty=nameWithType> must match before the provider retries the command. If the expression doesn't match, the command runs once without retry logic.

> [!NOTE]  
> The regular expression is case-sensitive. Include an inline option such as `(?i)` when you need case-insensitive matching.

### Examples

- Attempts to establish a connection up to three times with an approximate 1-second delay between tries by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateFixedRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

  ```xml
  <SqlConfigurableRetryLogicConnection retryMethod ="CreateFixedRetryProvider"
                                          numberOfTries ="3" deltaTime ="00:00:01"/>
  ```

- Attempts to establish a connection up to five times with up to a 45-second delay between tries by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateExponentialRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

  ```xml
  <SqlConfigurableRetryLogicConnection retryMethod ="CreateExponentialRetryProvider"
                      numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"/>
  ```

- Attempts to execute a command up to four times with a delay between 2 and 30 seconds by using the <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType> method and the default transient error list:

  ```xml
  <SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider"
                      numberOfTries ="4" deltaTime ="00:00:02" maxTime ="00:00:30"/>
  ```

- Attempts to execute a command up to eight times with a delay from one second to one minute. It's limited to commands with `CommandText` containing the uppercase word `SELECT` and error numbers 102 or 997. Specifying `transientErrors` replaces the built-in list, so no other errors are retried. The following example uses <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateIncrementalRetryProvider%2A?displayProperty=nameWithType>:

  ```xml
  <SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider"
                          numberOfTries ="8" deltaTime ="00:00:01" maxTime ="00:01:00"
                          transientErrors="102, 997"
                          authorizedSqlCondition="\b(SELECT)\b"/>
  ```

> [!NOTE]  
> In the next two samples, you can find the custom retry logic source code from [Configurable retry logic core APIs in SqlClient](configurable-retry-logic-core-apis-sqlclient.md#example). It's assumed the `CreateCustomProvider` method is defined in the `CustomCRL_Doc.CustomRetry` class in the `CustomCRL_Doc.dll` assembly that is in the application's executing directory.

- Attempts to establish a connection up to five times, with a delay between 3 and 45 seconds, error numbers 4060, 997, and 233 in the list, and using the specified custom retry provider:

  ```xml
  <SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                      retryMethod ="CreateCustomProvider"
                      numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                      transientErrors ="4060, 997, 233"/>
  ```

- This sample behaves like the previous one:

  ```xml
  <SqlConfigurableRetryLogicConnection retryLogicType ="CustomCRL_Doc.CustomRetry, CustomCRL_Doc"
                      retryMethod ="CreateCustomProvider"
                      numberOfTries ="5" deltaTime ="00:00:03" maxTime ="00:00:45"
                      transientErrors ="4060, 997, 233"/>
  ```

> [!IMPORTANT]  
> The first request for either configured default causes SqlClient to load and cache both the connection and command providers. Changes to the configuration file don't affect either provider until the process restarts.

Errors while reading retry settings don't fail the application. SqlClient traces the configuration error and uses <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.CreateNoneRetryProvider%2A?displayProperty=nameWithType>, so operations run without retries.

Use event source tracing to verify or troubleshoot retry configuration. For more information, see [Enable event tracing in SqlClient](enable-eventsource-tracing.md).

## Related content

- [Built-in retry logic providers in SqlClient](internal-retry-logic-providers-sqlclient.md)
- [Enable event tracing in SqlClient](enable-eventsource-tracing.md)
- [Specifying fully qualified type names](/dotnet/framework/reflection-and-codedom/specifying-fully-qualified-type-names)
- [Configurable retry logic in SqlClient](configurable-retry-logic.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
