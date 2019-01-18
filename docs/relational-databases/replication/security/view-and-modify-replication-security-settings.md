---
title: "View and Modify Replication Security Settings | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying replication security settings"
  - "replication [SQL Server], security"
  - "security [SQL Server replication], viewing settings"
  - "viewing replication security settings"
  - "security [SQL Server replication], modifying settings"
ms.assetid: 67d79532-1482-4de1-ac9f-4a23d162c85e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View and Modify Replication Security Settings
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to view and modify replication security settings in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO). For example, you might want to change the connection of the Log Reader Agent to the Publisher from SQL Server Authentication to Windows Integrated Authentication, or you might need to change the credentials used to run an agent job when the Windows account password has changed. For information about the permissions required by each agent, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To view and modify replication security settings, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
-   **Follow Up:**  [After you modify replication security settings](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The stored procedures that you use will depend on the type of agent and the type of server connection.  
  
-   The RMO classes and properties you use depend on the type of agent and the type of server connection.  
  
###  <a name="Security"></a> Security  
 For security reasons, the actual values of passwords are masked in result sets returned by replication stored procedures.  
  
####  <a name="Permissions"></a> Permissions  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 View and modify security settings in the following dialog boxes:  
  
1.  The **Update Replication Passwords** dialog box, which is available from the **Replication** folder of [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. If you change the password for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account or Windows account on a server in a replication topology, use this dialog box rather than updating the password for each agent that uses the account. If agents on more than one server use the same account, you must connect to each server and change the password. The password is updated in all of the places that replication uses the password. The password is not updated in other places, such as linked servers.  
  
2.  The **Agent Security** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
3.  The **Subscription Properties - \<Subscription>** dialog box. For more information about accessing this dialog box, see [View and Modify Push Subscription Properties](../../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
4.  The **Distributor Properties - \<Distributor>** and **Distribution Database Properties - \<Database>** dialog boxes. For more information about accessing these dialog boxes, see [View and Modify Distributor and Publisher Properties](../../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
5.  The **Publisher Properties - \<Publisher>** dialog box. For more information about accessing this dialog box, [View and Modify Distributor and Publisher Properties](../../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
#### To change the password for an account used by one or more agents  
  
1.  If the account is a SQL Server account, this dialog box will also change the password for the SQL Server account. If the account is a Windows account, change the password in Windows first. For more information, see the Windows documentation.  
  
    > [!NOTE]  
    >  After changing a replication password, you must stop and restart each agent that uses the password before the change takes effect for that agent.  
  
2.  Connect to the server in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
3.  Right-click the **Replication** folder, and then click **Update Replication Passwords**.  
  
4.  In the **Update Replication Passwords** dialog box, specify the account and the new password.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Snapshot Agent  
  
1.  On the **Agent Security** page of the **Publication Properties - \<Publication>** dialog box, click the **Security Settings** button next to the **Snapshot Agent** text box.  
  
2.  In the **Snapshot Agent Security** dialog box, specify the account under which the agent should run:  
  
    -   Enter a new Windows account in the **Agent account** text box.  
  
    -   Enter a new strong password in the **Password** and **Confirm Password** text boxes.  
  
3.  Specify the context under which the agent should connect from the Distributor to the Publisher. If you select **Using the following SQL Server login**, you must also specify the login:  
  
    -   Enter a login in the **Login** text box  
  
    -   Enter a new strong password in the **Password** and **Confirm Password** text boxes.  
  
    > [!NOTE]  
    >  If the Publisher is an Oracle Publisher, the connection context is specified in the **Distributor Properties - \<Distributor>**dialog box. See below for the procedure to change the context.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Log Reader Agent  
  
1.  On the **Agent Security** page of the **Publication Properties - \<Publication>** dialog box, click the **Security Settings** button next to the **Log Reader Agent** text box.  
  
2.  In the **Log Reader Agent Security** dialog box, specify the account under which the agent should run:  
  
    -   Enter a new Windows account in the **Agent account** text box  
  
    -   Enter a new strong password in the **Password** and **Confirm Password** text boxes.  
  
3.  Specify the context under which the agent should connect from the Distributor to the Publisher. If you select **Using the following SQL Server login**, you must also specify the login:  
  
    -   Enter a login in the **Login** text box  
  
    -   Enter a new strong password in the **Password** and **Confirm Password** text boxes.  
  
    > [!NOTE]  
    >  If the Publisher is an Oracle Publisher, the connection context is specified in the **Distributor Properties - \<Distributor>**dialog box. Change the context using the next procedure.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  There is one Log Reader Agent for each published database. Changing the security settings for the agent on one publication affects the settings for all publications in the publication database.  
  
#### To change the context under which the Snapshot Agent and Log Reader Agent for an Oracle publication make connections to the Publisher  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) next to a Publisher.  
  
2.  In the **Agent Connection to the Publisher** section, specify the login and password used by the replication administrative user schema you configured. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Distribution Agent for a push subscription  
  
1.  In the **Subscription Properties - \<Subscription>** dialog box at the Publisher, you can make the following changes:  
  
    -   To change the account under which the Distribution Agent runs and makes connections to the Distributor, click the **Agent process account** row, and then click the properties (**…**) button in the row. Specify an account and password in the **Distribution Agent Security** dialog box.  
  
    -   To change the context under which the Distribution Agent connects to the Subscriber, click the **Subscriber Connection** row, and then click the properties (**…**) button in the row. Specify the context in the **Enter Connection Information** dialog box.  
  
         If you use queued updating subscriptions, the Queue Reader Agent also uses the context specified here for connections to the Subscriber.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Distribution Agent for a pull subscription  
  
1.  In the **Subscription Properties - \<Subscription>** dialog box at the Subscriber, you can make the following changes:  
  
    -   To change the account under which the Distribution Agent runs and makes connections to the Subscriber, click the **Agent process account** row, and then click the properties (**…**) button in the row. Specify an account and password in the **Distribution Agent Security** dialog box.  
  
         If you use queued updating subscriptions, the Queue Reader Agent also uses the context specified here for connections to the Subscriber.  
  
    -   To change the context under which the Distribution Agent connects to the Distributor, click the **Distributor Connection** row, and then click the properties (**…**) button in the row. Specify the context in the **Enter Connection Information** dialog box.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Merge Agent for a push subscription  
  
1.  In the **Subscription Properties - \<Subscription>** dialog box at the Publisher, you can make the following changes:  
  
    -   To change the account under which the Merge Agent runs and makes connections to the Publisher and Distributor, click the **Agent process account** row, and then click the properties (**…**) button in the row. Specify an account and password in the **Merge Agent Security** dialog box.  
  
    -   To change the context under which the Merge Agent connects to the Subscriber, click the **Subscriber Connection** row, and then click the properties (**…**) button in the row. Specify the context in the **Enter Connection Information** dialog box.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change security settings for the Merge Agent for a pull subscription  
  
1.  In the **Subscription Properties - \<Subscription>** dialog box at the Subscriber, you can make the following changes:  
  
    -   To change the account under which the Merge Agent runs and makes connections to the Subscriber, click the **Agent process account** row, and then click the properties (**…**) button in the row. Specify an account and password in the **Merge Agent Security** dialog box.  
  
    -   To change the context under which the Merge Agent connects to the Publisher and Distributor, click the **Publisher Connection** row, and then click the properties (**…**) button in the row. Specify the context in the **Enter Connection Information** dialog box.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To change the account under which the Queue Reader Agent runs  
  
1.  On the **General** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties (**…**) button next to the distribution database.  
  
2.  In the **Distribution Database Properties - \<Database>** dialog box, click the **Security Settings** button next to the **Agent process account** text box.  
  
3.  In the **Queue Reader Agent Security** dialog box, specify the account under which the agent runs and makes connections to the Distributor:  
  
    -   Enter a new Windows account in the **Process account** text box  
  
    -   Enter a new strong password in the **Password** and **Confirm Password** text boxes.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  There is one Queue Reader Agent for each distribution database. Changing the security settings for the agent affects the settings for all publications at all Publishers that use this distribution database.  
  
#### To change the context under which the Queue Reader Agent makes connections to the Publisher  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click the properties button (**...**) next to the Publisher.  
  
2.  In the **Agent Connection to the Publisher** section, specify a value of **Impersonate the agent process account** or **SQL Server Authentication** for the **Agent Connection Mode** option. If you specify **SQL Server Authentication**, also enter values for **Login** and **Password**.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  There is one Queue Reader Agent for each distribution database. Changing the security settings for the agent affects the settings for all publications at all Publishers that use this distribution database.  
  
#### To change the context under which the Queue Reader Agent makes connections to the Subscriber  
  
-   The Queue Reader Agent uses the same connection context as the Distribution Agent for the subscription. For more information, see the procedures above for the Distribution Agent.  
  
#### To change security settings for an immediate updating pull subscription  
  
1.  In the **Subscription Properties - \<Subscription>** dialog box at the Subscriber, click the **Publisher Connection** row, and then click the properties (**…**) button in the row.  
  
2.  In the **Enter Connection Information** dialog box, select one of the following options:  
  
    -   **Use a login from a linked or remote server**. Select this option if you have defined a remote server or linked server between the Subscriber and the Publisher using [sp_addserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), [sp_addlinkedserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], or another method.  
  
    -   **Use SQL Server Authentication with the following login and password**. Select this option if you have not defined a remote server or linked server between the Subscriber and the Publisher. Replication will create a linked server for you. The account you specify must already exist at the Publisher.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
> [!NOTE]  
>  This procedure changes the method that replication triggers use to connect from the Subscriber to the Publisher when changes are made at the Subscriber. You can also change settings associated with the Distribution Agent for an immediate updating subscription. For more information, see the procedures earlier in this topic.  
>   
>  This procedure applies only to pull subscriptions. For push subscriptions, use the stored procedure [sp_link_publication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md).  
  
#### To change the password for the administrative connection from the Publisher to the Distributor  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, enter a strong password in the **Password** and **Confirm Password** text boxes.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
3.  On the **General** page of the **Publisher Properties - \<Publisher>** dialog box, enter a strong password in the **Password** and **Confirm Password** text boxes.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
> [!IMPORTANT]  
>  In all of the following procedures, when possible, prompt users to enter security credentials at runtime. If you store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
#### To change all instances of a stored password at a replication server  
  
1.  At a server in a replication topology on the master database, execute [sp_changereplicationserverpasswords](../../../relational-databases/system-stored-procedures/sp-changereplicationserverpasswords-transact-sql.md). Specify the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows account or [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login whose password is being changed for **@login** and new password for the account or login for **@password**. This changes every instance of the password used by all agents on the server when connecting to other servers in the topology.  
  
    > [!NOTE]  
    >  To only change the login and password for a connection to a particular server in the topology (such as the Distributor or Subscriber), specify this server's name for **@server**.  
  
2.  Repeat step 1 at every server in the replication topology where the password must be updated.  
  
    > [!NOTE]  
    >  After changing a replication password, you must stop and restart each agent that uses the password before the change takes effect for that agent.  
  
#### To change security settings for the Snapshot Agent  
  
1.  At the Publisher, execute [sp_helppublication_snapshot](../../../relational-databases/system-stored-procedures/sp-helppublication-snapshot-transact-sql.md), specifying **@publication**. This returns the current security settings for the Snapshot Agent.  
  
2.  At the Publisher, execute [sp_changepublication_snapshot](../../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md), specifying **@publication** and one or more of the following security settings to change:  
  
    -   To change the Windows account under which the agent runs or just password for this account, specify **@job_login** and **@job_password**.  
  
    -   To change the security mode used when connecting to the Publisher, specify a value of **1** or **0** for **@publisher_security_mode**.  
  
    -   When changing the security mode used when connecting to the Publisher from **1** to **0** or when changing a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login used for this connection, specify **@publisher_login** and **@publisher_password**.  
  
    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To change security settings for the Log Reader Agent  
  
1.  At the Publisher, execute [sp_helplogreader_agent](../../../relational-databases/system-stored-procedures/sp-helplogreader-agent-transact-sql.md), specifying **@publisher**. This returns the current security settings for the Log Reader Agent.  
  
2.  At the Publisher, execute [sp_changelogreader_agent](../../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md), specifying **@publication** and one or more of the following security settings to change:  
  
    -   To change the Windows account under which the agent runs or just password for this account, specify **@job_login** and **@job_password**.  
  
    -   To change the security mode used when connecting to the Publisher, specify a value of **1** or **0** for **@publisher_security_mode**.  
  
    -   When changing the security mode used when connecting to the Publisher from **1** to **0** or when changing a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login used for this connection, specify **@publisher_login** and **@publisher_password**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To change security settings for the Distribution Agent for a push subscription  
  
1.  At the Publisher on the publication database, execute [sp_helpsubscription](../../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md), specifying **@publication** and **@subscriber**. This returns subscription properties, including security settings for the Distribution Agent running at the Distributor.  
  
2.  At the Publisher on the publication database, execute [sp_changesubscription](../../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md), specifying **@publication**, **@subscriber**, **@subscriber_db**, a value of **all** for **@article**, the name of the security property for **@property**, and the new value of the property for **@value**.  
  
3.  Repeat step 2 for each of the following security properties being changed:  
  
    -   To change the Windows account under which the agent runs or just the password for this account, specify a value of **distrib_job_password** for **@property** and a new password for **@value**. When changing the account itself, repeat step 2 specifying a value of **distrib_job_login** for **@property** and the new Windows account for **@value**.  
  
    -   To change the security mode used when connecting to the Subscriber, specify a value of **subscriber_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Subscriber security mode to SQL Server Authentication, or if changing login information for SQL Server Authentication, specify a value of **subscriber_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **subscriber_login** for **@property** and the new login for **@value**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all properties, including **distrib_job_login** and **distrib_job_password**, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To change security settings for the Distribution Agent for a pull subscription  
  
1.  At the Subscriber, execute [sp_helppullsubscription](../../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md), specifying **@publication**. This returns subscription properties, including security settings for the Distribution Agent running at the Subscriber.  
  
2.  At the Subscriber on the subscription database, execute [sp_change_subscription_properties](../../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md), specifying **@publisher**, **@publisher_db**, **@publication**, the name of the security property for **@property**, and the new value of the property for **@value**.  
  
3.  Repeat step 2 for each of the following security properties being changed:  
  
    -   To change the Windows account under which the agent runs or just the password for this account, specify a value of **distrib_job_password** for **@property** and a new password for **@value**. When changing the account itself, repeat step 2 specifying a value of **distrib_job_login** for **@property** and the new Windows account for **@value**.  
  
    -   To change the security mode used when connecting to the Distributor, specify a value of **distributor_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Distributor security mode to SQL Server Authentication or if changing login information for SQL Server Authentication, specify a value of **distributor_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **distributor_login** for **@property** and the new login for **@value**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
#### To change security settings for the Merge Agent for a push subscription  
  
1.  At the Publisher on the publication database, execute [sp_helpmergesubscription](../../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md), specifying **@publication**, **@subscriber**, and **@subscriber_db**. This returns subscription properties, including security settings for the Merge Agent running at the Distributor.  
  
2.  At the Publisher on the publication database, execute [sp_changemergesubscription](../../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md), specifying **@publication**, **@subscriber**, **@subscriber_db**, the name of the security property for **@property**, and the new value of the property for **@value**.  
  
3.  Repeat step 2 for each of the following security properties being changed:  
  
    -   To change the Windows account under which the agent runs, or just the password for this account, specify a value of **merge_job_password** for **@property** and a new password for **@value**. When changing the account itself, repeat step 2 specifying a value of **merge_job_login** for **@property** and the new Windows account for **@value**.  
  
    -   To change the security mode used when connecting to the Subscriber, specify a value of **subscriber_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Subscriber security mode to SQL Server Authentication, or if changing login information for SQL Server Authentication, specify a value of **subscriber_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **subscriber_login** for **@property** and the new login for **@value**.  
  
    -   To change the security mode used when connecting to the Publisher, specify a value of **publisher_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Publisher security mode to SQL Server Authentication, or if changing login information for SQL Server Authentication, specify a value of **publisher_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **publisher_login** for **@property** and the new login for **@value**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all properties, including **merge_job_login** and **merge_job_password**, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To change security settings for the Merge Agent for a pull subscription  
  
1.  At the Subscriber, execute [sp_helpmergepullsubscription](../../../relational-databases/system-stored-procedures/sp-helpmergepullsubscription-transact-sql.md), specifying **@publication**. This returns subscription properties, including security settings for the Merge Agent running at the Subscriber.  
  
2.  At the Subscriber on the subscription database, execute [sp_change_subscription_properties](../../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md), specifying **@publisher**, **@publisher_db**, **@publication**, the name of the security property for **@property**, and the new value of the property for **@value**.  
  
3.  Repeat step 2 for each of the following security properties being changed:  
  
    -   To change the Windows account under which the agent runs or just the password for this account, specify a value of **merge_job_password** for **@property** and new password for **@value**. When changing the account itself, repeat Step 2 specifying a value of **merge_job_login** for **@property** and the new Windows account for **@value**.  
  
    -   To change the security mode used when connecting to the Distributor, specify a value of **distributor_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Distributor security mode to SQL Server Authentication or if changing login information for SQL Server Authentication, specify a value of **distributor_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **distributor_login** for **@property** and the new login for **@value**.  
  
    -   To change the security mode used when connecting to the Publisher, specify a value of **publisher_security_mode** for **@property** and a value of **1** (Windows Integrated Authentication) or **0** (SQL Server Authentication) for **@value**.  
  
    -   When changing the Publisher security mode to SQL Server Authentication or if changing login information for SQL Server Authentication, specify a value of **publisher_password** for **@property** and the new password for **@value**. Repeat step 2, specifying a value of **publisher_login** for **@property** and the new login for **@value**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
#### To change security settings for the Snapshot Agent to generate a filtered snapshot for a Subscriber  
  
1.  At the Publisher, execute [sp_helpdynamicsnapshot_job](../../../relational-databases/system-stored-procedures/sp-helpdynamicsnapshot-job-transact-sql.md), specifying **@publication**. In the result set, note the value of **job_name** for the Subscriber's partition to change.  
  
2.  At the Publisher, execute [sp_changedynamicsnapshot_job](../../../relational-databases/system-stored-procedures/sp-changedynamicsnapshot-job-transact-sql.md), specifying **@publication**, the value obtained from step 1 for **@dynamic_snapshot_jobname**, and a new password for **@job_password** or login and password for the Windows account under which the agent runs for **@job_login** and **@job_password**.  
  
    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To change security settings for the Queue Reader Agent  
  
1.  At the Distributor, execute [sp_helpqreader_agent](../../../relational-databases/system-stored-procedures/sp-helpqreader-agent-transact-sql.md). This returns the current Windows account under which the Queue Reader Agent runs.  
  
    -   At the Distributor, execute [sp_changeqreader_agent](../../../relational-databases/system-stored-procedures/sp-changeqreader-agent-transact-sql.md), specifying the Windows account settings for **@job_login** and **@job_passwsord**.  
  
    > [!NOTE]  
    >  After changing an agent login or password, you must stop and restart the agent before the change takes effect. There is one Queue Reader Agent for each distribution database. Changing the security settings for the agent affects the settings for all publications at all Publishers that use this distribution database.  
  
2.  The Queue Reader Agent makes connections to the Subscriber using the same connection context as the Distribution Agent for the subscription.  
  
#### To change security mode used by an immediate updating Subscriber when connecting to the Publisher  
  
1.  At the Subscriber on the subscription database, execute [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md). Specify **@publisher**, **@publication**, the name of the publication database for **@publisher_db**, and one of the following values for **@security_mode**:  
  
    -   **0** to use SQL Server Authentication when making updates at the Publisher. This option requires you to specify a valid login at the Publisher for **@login** and **@password**.  
  
    -   **1** to use the security context of the user making changes at the Subscriber when connecting to the Publisher. See [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) for restrictions related to this security mode.  
  
    -   **2** to use an existing, user-defined linked server login created using [sp_addlinkedserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  
  
#### To change the password for a remote Distributor  
  
1.  At the Distributor on the distribution database, execute [sp_changedistributor_password](../../../relational-databases/system-stored-procedures/sp-changedistributor-password-transact-sql.md), specifying the new password for this login for **@password**.  
  
    > [!IMPORTANT]  
    >  Do not change the password for **distributor_admin** directly.  
  
2.  At every Publisher that uses this remote Distributor, execute [sp_changedistributor_password](../../../relational-databases/system-stored-procedures/sp-changedistributor-password-transact-sql.md), specifying the password from step 1 for **@password**.  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](https://go.microsoft.com/fwlink/?LinkId=34733) provided by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows .NET Framework.  
  
#### To change all instances of a password stored on a replication server  
  
1.  Create a connection to the replication server by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class by using the connection from step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.ChangeReplicationServerPasswords%2A> method. Specify the following parameters:  
  
    -   *security_mode* - a <xref:Microsoft.SqlServer.Replication.ReplicationSecurityMode> value that specifies the type of authentication for which all instances of the password are being changed.  
  
    -   *login* - the login for which all instances of the password are being changed.  
  
    -   *password* - the new password value.  
  
        > [!IMPORTANT]  
        >  When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](https://go.microsoft.com/fwlink/?LinkId=34733) provided by the Windows .NET Framework.  
  
        > [!NOTE]  
        >  Only a member of the **sysadmin** fixed server role can call this method.  
  
4.  Repeat steps 1-3 at every server in the replication topology where the password must be updated.  
  
#### To change security settings for the Distribution Agent for a push subscription to a transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A> properties for the subscription, and set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
5.  Set one or more of the following security properties on the instance of <xref:Microsoft.SqlServer.Replication.TransSubscription>:  
  
    -   To change the credentials for the Windows account under which the agent runs, set the <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A>.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Subscriber, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Subscriber, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> property to **false**, and specify the Subscriber login credentials for the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
        > [!NOTE]  
        >  The agent connection to the Distributor is always made using the Windows credentials specified by <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A>. This account is also used to make remote connections using Windows Authentication.  
  
6.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To change security settings for the Distribution Agent for a pull subscription to a transactional publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties for the subscription, and set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
5.  Set one or more of the following security properties on the instance of <xref:Microsoft.SqlServer.Replication.TransPullSubscription>:  
  
    -   To change the credentials for the Windows account under which the agent runs, set the <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A>.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Distributor, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Distributor, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> property to **false**, and specify the Distributor login credentials for the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
        > [!NOTE]  
        >  The agent connection to the Subscriber is always made using the Windows credentials specified by <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A>. This account is also used to make remote connections using Windows Authentication.  
  
6.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To change security settings for the Merge Agent for a pull subscription to a merge publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties for the subscription, and set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
5.  Set one or more of the following security properties on the instance of <xref:Microsoft.SqlServer.Replication.MergePullSubscription>:  
  
    -   To change the credentials for the Windows account under which the agent runs, set the <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A>.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Distributor, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Distributor, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> property to **false**, and specify the Distributor login credentials for the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Publisher, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Publisher, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherSecurity%2A> property to **false**, and specify the Publisher login credentials for the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
        > [!NOTE]  
        >  The agent connection to the Subscriber is always made using the Windows credentials specified by <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A>. This account is also used to make remote connections using Windows Authentication.  
  
6.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To change security settings for the Merge Agent for a push subscription to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A> properties for the subscription, and set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
5.  Set one or more of the following security properties on the instance of <xref:Microsoft.SqlServer.Replication.MergeSubscription>:  
  
    -   To change the credentials for the Windows account under which the agent runs, set the <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A>.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Subscriber, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Subscriber, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> property to **false**, and specify the Subscriber login credentials for the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
    -   To specify Windows Integrated Authentication as the type of authentication that the agent uses when it connects to the Publisher, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.MergeSubscription.PublisherSecurity%2A> property to **true**.  
  
    -   To specify SQL Server Authentication as the type of authentication that the agent uses when it connects to the Publisher, set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> field of the <xref:Microsoft.SqlServer.Replication.MergeSubscription.PublisherSecurity%2A> property to **false**, and specify the Publisher login credentials for the <xref:Microsoft.SqlServer.Replication.PublisherConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.PublisherConnectionSecurityContext.SqlStandardPassword%2A> fields.  
  
        > [!NOTE]  
        >  The agent connection to the Distributor is always made using the Windows credentials specified by <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A>. This account is also used to make remote connections using Windows Authentication.  
  
6.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To change the login information used by an immediate updating Subscriber when it connects to the transactional publisher  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase> class for the subscription database. Specify <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.Name%2A> and the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the database properties in step 2 were defined incorrectly or the subscription database does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.LinkPublicationForUpdateableSubscription%2A> method, passing the following parameters:  
  
    -   *Publisher* - the name of the Publisher.  
  
    -   *PublisherDB* - the name of the publication database.  
  
    -   *Publication* - the name of the publication to which the immediate updating Subscriber is subscribed.  
  
    -   *Distributor* - the name of the Distributor.  
  
    -   *PublisherSecurity* - A <xref:Microsoft.SqlServer.Replication.PublisherConnectionSecurityContext> object that specifies the type of security mode used by the immediate updating Subscriber when connecting to the Publisher and login credentials for the connection.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example checks the supplied login value and changes all passwords for the supplied Windows login or SQL Server login stored by replication on the server.  
  
 [!code-cs[HowTo#rmo_ChangeServerPasswords](../../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_changeserverpasswords)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeServerPasswords](../../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_changeserverpasswords)]  
  
##  <a name="FollowUp"></a> Follow Up: After you modify replication security settings  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## See Also  
 [Replication Management Objects Concepts](../../../relational-databases/replication/concepts/replication-management-objects-concepts.md)   
 [Upgrade Replication Scripts &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/upgrade-replication-scripts-replication-transact-sql-programming.md)   
 [Identity and access control for replication](../../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
  
  
