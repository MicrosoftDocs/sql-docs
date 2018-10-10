---
title: "Creating a Login | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "creating a login"
ms.assetid: a2512310-bdb6-41dc-858a-e866b2b58afc
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Creating a Login
  To access the [!INCLUDE[ssDE](../includes/ssde-md.md)], users require a login. The login can represent the user's identity as a Windows account or as a member of a Windows group, or the login can be a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] login that exists only in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Whenever possible you should use Windows Authentication.  
  
 By default, administrators on your computer have full access to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For this lesson, we want to have a less privileged user; therefore, you will create a new local Windows Authentication account on your computer. To do this, you must be an administrator on your computer. Then you will grant that new user access to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
### To create a new Windows account  
  
1.  Click **Start**, click **Run**, in the **Open** box, type `%SystemRoot%\system32\compmgmt.msc /s`, and then click **OK** to open the Computer Management program.  
  
2.  Under **System Tools**, expand **Local Users and Groups**, right-click **Users**, and then click **New User**.  
  
3.  In the **User name** box type **Mary**.  
  
4.  In the **Password** and **Confirm password** box, type a strong password, and then click **Create** to create a new local Windows user.  
  
### To create a login  
  
1.  In a Query Editor window of [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], type and execute the following code replacing `computer_name` with the name of your computer. `FROM WINDOWS` indicates that Windows will authenticate the user. The optional `DEFAULT_DATABASE` argument connects `Mary` to the `TestData` database, unless her connection string indicates another database. This statement introduces the semicolon as an optional termination for a [!INCLUDE[tsql](../includes/tsql-md.md)] statement.  
  
    ```  
    CREATE LOGIN [computer_name\Mary]  
        FROM WINDOWS  
        WITH DEFAULT_DATABASE = [TestData];  
    GO  
    ```  
  
     This authorizes a user name `Mary`, authenticated by your computer, to access this instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If there is more than one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the computer, you must create the login on each instance that `Mary` must access.  
  
    > [!NOTE]  
    >  Because `Mary` is not a domain account, this user name can only be authenticated on this computer.  
  
## Next Task in Lesson  
 [Granting Access to a Database](lesson-2-2-granting-access-to-a-database.md)  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-login-transact-sql)   
 [Choose an Authentication Mode](../relational-databases/security/choose-an-authentication-mode.md)  
  
  
