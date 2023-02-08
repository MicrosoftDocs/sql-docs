## SQL Server metadata

Metadata information about SQL Server instances and databases is collected after the Azure extension for SQL Server is installed. 

### Instance metadata the extension sents to Azure

The instance ineventory is collected and sent automatically for each instance on a connected machine unless the instace is listed as excluded in the extension configuration. 

| Metadata | Property name | Sample value |
|--|--|--|
| Computer name | name | "SQL22-EE_PAYGTEST" |
| SQL Server instance name| instanceName | "PAYGTEST"
| SQL Server Version | version | "SQL Server 2022" |
| SQL Server Edition | edition | "Enterprise" |
| Containing server resource ID | containerResourceId |  "/subscriptions/.../SQL22-EE"
| Virtual cores | vCore | "8" |
| Connecticity status | status | "Connected" |
| SQL Server patch level | patchLevel | "14.0.2042.3" |
| Collation | collation | "SQL_Latin1_General_CP1_CI_AS" |
| Current version | currentVersion | "14.0.1000.169" |
| TCP dynamic ports | tcpDynamicPorts | "61394" |
| TCP static ports | tcpStaticPorts | "1433" |
| Product ID | productId | "00488-0000-00000-AB944" |
| License type | licenseType | "PAYG" |
| Microsoft Defender status | azureDefenderStatus | "Protected" |
| Microsoft Defender status last updated | azureDefenderStatusLastUpdated | "2023-02-08T02:57:35.1059718Z" |

### Database metadata the extension sents to Azure

The database inventory is an optional feature. The database metedata is collected and sent only if the user enables it. The following information is collected:

* Resource location (region)
* Virtual machine ID
* Tags
* Azure Active Directory managed identity certificate
* Guest configuration policy assignments
* Extension requests - install, update, and delete.

### Azure metadata the extension receives from Azure

* Resource location (region)
* Virtual machine ID
* Tags
* Azure Active Directory managed identity certificate
* Guest configuration policy assignments
* Extension requests - install, update, and delete.

SubscriptionId
ResouceGroup
Instance Resource ID
Resource Location
Resource Tags
Created Date
Modified Date



> [!NOTE]
> Azure Arc-enabled servers doesn't store/process customer data outside the region the customer deploys the service instance in.
