---
title: Configure usage and diagnostic data collection for SQL Server (CEIP) | Microsoft Docs
description: Learn about the information that SQL Server collects from users to improve products. See how to configure SQL Server not to send this information.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 08/26/2021
ms.topic: conceptual
ms.service: sql
ms.custom: ""
ms.subservice: configuration
---
# Configure usage and diagnostic data collection for SQL Server (CEIP)

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

## Summary

By default, Microsoft SQL Server collects information about how its customers are using the application. Specifically, SQL Server collects information about the installation experience, usage, and performance. This information helps Microsoft improve the product to better meet customer needs. For example, Microsoft collects information about what kinds of error codes customers encounter so that we can fix related bugs, improve our documentation about how to use SQL Server, and determine whether features should be added to the product to better serve customers.

Specifically, Microsoft does not send any of the following types of information through this mechanism:
- Any values from inside user tables
- Any logon credentials or other authentication information
- Personal information

The following sample scenario includes feature usage information that helps improve the product.

SQL Server 2017 and later support columnstore indexes to enable fast analytics scenarios. Columnstore indexes combine a traditional B-tree index structure for newly inserted data with a special column-oriented compressed structure to compress data and speed query execution. The product contains heuristics to migrate data from the B-tree structure to the compressed structure in the background, thereby speeding up future query results.

[!INCLUDE [sql-b-tree](../includes/sql-b-tree.md)]

If the background operation does not keep pace with the rate at which data is inserted, query performance may be slower than expected. To improve the product, Microsoft collects information about how well SQL Server is keeping up with the automatic data compression process. The product team uses this information to fine-tune the frequency and parallelism of the code that performs compression. This query is run occasionally to collect this information so that we (Microsoft) can evaluate the data movement rate. This helps us optimize the product heuristics.  

```sql
SELECT object_id, type_desc, data_space_id, db_id() AS database_id FROM sys.indexes WITH(nolock) WHERE type = 5 or type = 6 
```

```sql
SELECT cntr_value as merge_policy_evaluation
FROM sys.dm_os_performance_counters WITH(nolock)
WHERE object_name LIKE '%columnstore%' 
AND counter_name ='Total Merge Policy Evaluations' 
AND instance_name = '_Total'
```

Be aware that this process focuses on the necessary mechanisms for delivering value to customers. The product team does not look at the data in the index or send that data to Microsoft. 
SQL Server always collects and sends information about the installation experience from the setup process so that we can quickly find and fix any installation problems that the customer is experiencing. SQL Server 2017 and later can be configured not to send information (on a per-server instance basis) to Microsoft through the following mechanisms:
- By using the Error and Usage Reporting application
- By setting registry subkeys on the server

For SQL Server on Linux refer to [Customer Feedback for SQL Server on Linux](../linux/usage-and-diagnostic-data-configuration-for-sql-server-linux.md)

> [!NOTE]
> You can disable the sending of information to Microsoft only in paid versions of SQL Server.

## Remarks
 - Removing or disabling the SQL CEIP service is not supported. 
 - Removing the SQL CEIP resources from the Cluster Group is not supported. 

To opt out of the data collection, see [Turning local audit on or off](usage-and-diagnostic-data-in-local-audit.md#turning-local-audit-on-or-off)

## Error and Usage Reporting application 

After setup, the usage and diagnostic data collection setting for SQL Server components and instances can be changed through the Error and Usage Reporting application. This application is available as part of SQL Server installation. This tool lets each SQL Server instance configure its own Usage Reports setting.

> [!NOTE]
> The Error and Usage Reporting application is listed under the Configuration Tools of SQL Server. You can use this tool to manage your preference for Error Reporting and Usage and Diagnostic Data collection in the same manner as in SQL Server 2017. Error Reporting is separate from Usage and Diagnostic Data collection, therefore can be turned on or off independently from Usage and Diagnostic Data collection. Error Reporting collects crash dumps that are sent to Microsoft and that may contain sensitive information as outlined in the [Privacy Statement](./sql-server-privacy.md).
> 
> The Error and Usage Reporting application is not included in the SQL Server Reporting Services 2017 and later setup. The only mechanism available to configure sending information to Microsoft is by setting registry subkeys on the server.

To start SQL Server Error and Usage Reporting, select **Start**, and then search on "Error" in the search box. The SQL Server Error and Usage Reporting item will be displayed. After you start the tool, you can manage usage and diagnostic data as well as serious errors that are collected for instances and components that are installed on that computer.

For paid versions, use the "Usage Reports" check boxes to manage sending usage and diagnostic data to Microsoft.

For paid or free versions, use the "Error Reports" check boxes to manage sending feedback on serious errors and crash dumps to Microsoft.

## Set registry subkeys on the server

Enterprise customers can configure Group Policy settings to opt in or out of usage and diagnostic data collection. This is done by configuring a registry-based policy. The relevant registry subkey and settings are as follows:

- For SQL Server instance features:
    
    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\\{InstanceID}\CPE
    
    RegEntry name = CustomerFeedback
    
    Entry type DWORD: 0 is opt out; 1 is opt in
    
    {InstanceID} refers to the instance type and instance, as in the following examples:

    - MSSQL14.CANBERRA for SQL Server 2017 Database engine and Instance name of "CANBERRA"
    - MSAS14.CANBERRA for SQL Server 2017 Analysis Services and Instance name of "CANBERRA"

- For SQL Server Reporting Services 2017 and later instance features:

    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\SSRS\CPE
    
    RegEntry name = CustomerFeedback
    
    Entry type DWORD: 0 is opt out; 1 is opt in

- For all shared features:
    
    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\\{Major Version}
    
    RegEntry name = CustomerFeedback
    
    Entry type DWORD: 0 is opt out; 1 is opt in

> [!NOTE]
> {Major Version} refers to the version of SQL Server. For example, "140" refers to SQL Server 2017.

- For SQL Server Management Studio 17 and SQL Server Management Studio 18, refer to [User Assistance in SQL Server Management Studio](../ssms/sql-server-management-studio-telemetry-ssms.md)

## Set registry subkeys for crash dump collection

Similar to the behavior in an earlier version of SQL Server, SQL Server 2017 and later Enterprise edition customers can configure Group Policy settings on the server to opt in or out of crash dump collection. This is done by configuring a registry-based policy. The relevant registry subkeys and settings are as follows: 

- For SQL Server instance features:

    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\\{InstanceID}\CPE

    RegEntry name = EnableErrorReporting

    Entry type DWORD: 0 is opt out; 1 is opt-in
 
    {InstanceID} refers to the instance type and instance, as in the following examples: 

    - MSSQL14.CANBERRA for SQL Server 2017 Database engine and Instance name of "CANBERRA"
    - MSAS14.CANBERRA for SQL Server 2017 Analysis Services and Instance name of "CANBERRA"
 
- For SQL Server Reporting Services 2017 and later instance features:

    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\SSRS\CPE
    
    RegEntry name = EnableErrorReporting
    
    Entry type DWORD: 0 is opt out; 1 is opt in
    
- For all shared features:
    
    Subkey = HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SQL Server\\{Major Version}

    RegEntry name = EnableErrorReporting

    Entry type DWORD: 0 is opt out; 1 is opt-in

> [!NOTE]
> {Major Version} refers to the version of SQL Server. For example, "140" refers to SQL Server 2017.

Registry-based Group Policy on these registry subkeys is honored by SQL Server crash dump collection. 

## Crash dump collection for SSMS
SQL Server Management Studio (SSMS) doesn't collect its own crash dump. Any crash dump that's related to SSMS is collected as part of Windows Error Reporting.

The procedure to turn this feature on or off is dependent on the OS version. To turn the feature on or off, follow the steps in the appropriate article for your Windows version.
 
- Windows Server 2016 and later, and Windows 10 and later
    [Configure Windows diagnostic data in your organization](/windows/privacy/configure-windows-diagnostic-data-in-your-organization)

- Windows Server 2008 R2 and Windows 7
    [WER Settings](/windows/desktop/wer/wer-settings)
 
## Feedback for Analysis Services

During installation, SQL Server 2016 and later Analysis Services adds a special account to your Analysis Services instance. This account is a member of the Analysis Services Server Admin role. The account is used to collect information for feedback from the Analysis Services instance.  

You can configure your service not to send usage and diagnostic data, as described in the "Set registry subkeys on the server" section. However, doing this does not remove the service account. 
 
[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
