---
description: "Distribution Agent Security"
title: "Distribution Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.security.DA.f1"
helpviewer_keywords: 
  - "Distribution Agent Security dialog box"
ms.assetid: de40cc21-2e58-4464-9be7-b5b90c925e9b
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Distribution Agent Security
::: moniker range=">=sql-server-2016"
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
The **Distribution Agent Security** dialog box allows you to specify the Windows account under which the Distribution Agent runs. The Distribution Agent runs at the Distributor for push subscriptions and at the Subscriber for pull subscriptions. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account is also referred to as the *process account*, because the agent process runs under this account. Additional options available in the dialog box depend on how you access it:  
  
-   If the dialog box is accessed from the New Subscription Wizard, it also allows you to specify the context under which the Distribution Agent makes connections to the Subscriber (for push subscriptions) or the Distributor (for pull subscriptions). The connection can be made by impersonating the Windows account or under the context of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account you specify.  
  
-   If the dialog box is accessed from the **Subscription Properties** dialog box, specify the context under which the Distribution Agent makes connections by clicking the properties button (**...**) in the **Subscriber Connection** or **Distributor Connection** row of that dialog box. For more information about accessing the **Subscription Properties** dialog box, see [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and how to: [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
 All accounts must be valid, with the correct password specified for each account. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process Account**  
 Enter a Windows account under which the Distribution Agent runs:  
  
-   For push subscriptions, the account must:  
  
    -   At minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
    -   Be a member of the publication access list (PAL).  
  
    -   Have read permissions on the snapshot share.  
  
    -   Have read permissions on the install directory of the OLE DB provider for the Subscriber if the subscription is for a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber.  
  
-   For pull subscriptions, the account must at minimum be a member of the **db_owner** fixed database role in the subscription database.  
  
 Additional permissions are required if the process account is impersonated when connections are made. See the **Connect to the Distributor** and **Connect to the Subscriber** sections below.  
  
 **Process Account** cannot be specified for pull subscriptions to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], because the Distribution Agent does not run on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
 **Password** and **Confirm Password**  
 Enter the password for the Windows account.  
  
 **Connect to the Distributor**  
 For push subscriptions, connections to the Distributor are always made by impersonating the account specified in the **Process account** text box.  
  
 For pull subscriptions, select whether the Distribution Agent should make connections to the Distributor by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
> [!NOTE]  
>  It is recommended that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
 The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection must:  
  
-   Be a member of the PAL.  
  
-   Have read permissions on the snapshot share.  
  
 **Connect to the Subscriber**  
 For pull subscriptions, connections to the Subscriber are always made by impersonating the account specified in the **Process account** text box.  
  
 For push subscriptions, the options are different for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers and non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers:  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers: select whether the Distribution Agent should make connections to the Subscriber by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
    > [!NOTE]  
    >  It is recommended that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
     The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection to the Subscriber must at minimum be a member of the **db_owner** fixed database role in the subscription database.  
  
-   For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, specify the database login at the Subscriber that should be used when the Distribution Agent connects to the Subscriber. The login should have sufficient permissions to create objects in the subscription database. For more information about configuring non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, see [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md).  
  
 **Additional connection options**  
 Non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers only. Specify any connection options for the Subscriber in the form of a connection string (Oracle does not require additional options). Each option should be separated by a semi-colon. The following is an example of an IBM DB2 connection string (line breaks are for readability):  
  
```  
Provider=DB2OLEDB;Initial Catalog=MY_SUBSCRIBER_DB;Network Transport Library=TCP;Host CCSID=1252;  
PC Code Page=1252;Network Address=MY_SUBSCRIBER;Network Port=50000;Package Collection=MY_PKGCOL;  
Default Schema=MY_SCHEMA;Process Binary as Character=False;Units of Work=RUW;DBMS Platform=DB2/NT;  
Persist Security Info=False;Connection Pooling=True;  
```  
  
 Most of the options in the string are specific to the DB2 server you are configuring, but the **Process Binary as Character** option should always be set to **False**. A value is required for the **Initial Catalog** option to identify the subscription database. For more information, see [IBM DB2 Subscribers](../../relational-databases/replication/non-sql/ibm-db2-subscribers.md).  
  
## See Also  
 [Identity and access control for replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
::: moniker-end
  
::: moniker range="azuresqldb-mi-current"
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
The **Distribution Agent Security** dialog box allows you to specify the SQL authentication account under which the Distribution Agent runs. The Distribution Agent runs at the Distributor for push subscriptions and at the Subscriber for pull subscriptions.  Additional options available in the dialog box depend on how you access it:  
  
-   If the dialog box is accessed from the New Subscription Wizard, it also allows you to specify the context under which the Distribution Agent makes connections to the Subscriber (for push subscriptions) or the Distributor (for pull subscriptions). The connection should be made using a SQL Server authentication account. 
  
-   If the dialog box is accessed from the **Subscription Properties** dialog box, specify the context under which the Distribution Agent makes connections by clicking the properties button (**...**) in the **Subscriber Connection** or **Distributor Connection** row of that dialog box. For more information about accessing the **Subscription Properties** dialog box, see [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and how to: [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
 All accounts must be valid, with the correct password specified for each account. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process Account**  
 Enter a SQL Server authentication account under which the Distribution Agent runs:  
  
-   For push subscriptions, the account must:  
  
    -   At minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
    -   Be a member of the publication access list (PAL).  
  
    -   Have read permissions on the snapshot share.  
  
    -   Have read permissions on the install directory of the OLE DB provider for the Subscriber if the subscription is for a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber.  
  
-   For pull subscriptions, the account must at minimum be a member of the **db_owner** fixed database role in the subscription database.  
  
    
**Password** and **Confirm Password**  
Enter the password for the Windows account.  
  
**Connect to the Distributor**  
For push subscriptions, connections to the Distributor are always made by impersonating the account specified in the **Process account** text box.  
  
For pull subscriptions, select whether the Distribution Agent should make connections to the Distributor by impersonating the account specified in the **Process account** text box or by a SQL Server authentication account. 
  
  
 **Connect to the Subscriber**  
 For pull subscriptions, connections to the Subscriber are always made by impersonating the account specified in the **Process account** text box.  
  
 For push subscriptions, the options are different for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers and non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers:

  
-   For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, specify the database login at the Subscriber that should be used when the Distribution Agent connects to the Subscriber. The login should have sufficient permissions to create objects in the subscription database. For more information about configuring non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, see [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md).  
  
 **Additional connection options**  
 Non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers only. Specify any connection options for the Subscriber in the form of a connection string (Oracle does not require additional options). Each option should be separated by a semi-colon. The following is an example of an IBM DB2 connection string (line breaks are for readability):  
  
```  
Provider=DB2OLEDB;Initial Catalog=MY_SUBSCRIBER_DB;Network Transport Library=TCP;Host CCSID=1252;  
PC Code Page=1252;Network Address=MY_SUBSCRIBER;Network Port=50000;Package Collection=MY_PKGCOL;  
Default Schema=MY_SCHEMA;Process Binary as Character=False;Units of Work=RUW;DBMS Platform=DB2/NT;  
Persist Security Info=False;Connection Pooling=True;  
```  
  
 Most of the options in the string are specific to the DB2 server you are configuring, but the **Process Binary as Character** option should always be set to **False**. A value is required for the **Initial Catalog** option to identify the subscription database. For more information, see [IBM DB2 Subscribers](../../relational-databases/replication/non-sql/ibm-db2-subscribers.md).  
  
## See Also  
 [Transactional replication with Azure SQL Database](/azure/sql-database/sql-database-managed-instance-transactional-replication)
 [Configure replication for Azure SQL Managed Instance](/azure/sql-database/replication-with-sql-database-managed-instance)
::: moniker-end


