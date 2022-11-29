---
title: "Deploying CLR Database Objects"
description: Using Microsoft Visual Studio, you can develop CLR database objects for SQL Server, deploy them to a test server, and distribute them to production servers.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
ms.custom: intro-deployment
helpviewer_keywords:
  - "deployment script [CLR integration]"
  - "common language runtime [SQL Server], deploying"
  - "deploying assemblies [CLR integration]"
  - "deploying [CLR integration]"
ms.assetid: 00752573-3367-41a7-af98-7b7a29e8e2f2
---
# Deploying CLR Database Objects
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Deployment is the process by which you distribute a finished application or module to be installed and run on another computer. Using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio, you can develop common language runtime (CLR) database objects and deploy them to a test server. Alternatively, the managed database objects can also be compiled with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework redistribution files, instead of Visual Studio. Once compiled, the assemblies containing the CLR database objects can then be deployed to a test server using Visual Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Note that Visual Studio .NET 2003 cannot be used for CLR integration programming or deployment. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the .NET Framework pre-installed, and Visual Studio .NET 2003 cannot use the .NET Framework 2.0 assemblies.  
  
 Once the CLR methods have been tested and verified on the test server, they can be distributed to production servers using a deployment script. The deployment script can be generated manually, or by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (see the procedure later in this topic).  
  
 The CLR integration feature is turned off by default in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and must be enabled in order to use CLR assemblies. For more information, see [Enabling CLR Integration](../../relational-databases/clr-integration/clr-integration-enabling.md).  
  
## Deploying the Assembly to the Test Server  
 Using Visual Studio, you can develop CLR functions, procedures, triggers, user-defined types (UDTs), or user-defined aggregates (UDAs), and deploy them to a test server. These managed database objects can also be compiled with the command line compilers, such as csc.exe and vbc.exe, included with the .NET Framework redistribution files. The Visual Studio Integrated Development Environment is not required to develop managed database objects for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Make sure that all compiler errors and warnings are resolved. The assemblies containing the CLR routines can then be registered in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using Visual Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
> [!NOTE]  
>  The TCP/IP network protocol must be enabled on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance in order to use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio for remote development, debugging, and development. For more information about enabling TCP/IP protocol on the server, see [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md).  
  
#### To deploy the assembly using Visual Studio  
  
1.  Build the project by selecting **Build** \<project name> from the **Build** menu.  
  
2.  Resolve all build errors and warnings before deploying the assembly to the test server.  
  
3.  Select **Deploy** from the **Build** menu. The assembly will then be registered in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and database specified when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] project was first created in Visual Studio.  

#### To deploy the assembly using Transact-SQL  
  
1.  Compile the assembly from the source file using the command line compilers included with the .NET Framework.  
  
2.  For [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# source files:  
  
3.  `csc /target:library C:\helloworld.cs`  
  
4.  For [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic source files:  
  
 `vbc /target:library C:\helloworld.vb`  
  
 These commands launch the Visual C# or Visual Basic compiler using the **/target** option to specify building a library DLL.  
  
1.  Resolve all build errors and warnings before deploying the assembly to the test server.  
  
2.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on the test server. Create a new query, connected to a suitable test database (such as AdventureWorks).  
  
3.  Create the assembly in the server by adding the following [!INCLUDE[tsql](../../includes/tsql-md.md)] to the query.  
  
 `CREATE ASSEMBLY HelloWorld from 'c:\helloworld.dll' WITH PERMISSION_SET = SAFE;`  
  
1.  The procedure, function, aggregate, user-defined type, or trigger must then be created in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the **HelloWorld** assembly contains a method named **HelloWorld** in the **Procedures** class, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] can be added to the query to create a procedure called **hello** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 `CREATE PROCEDURE hello`  
  
 `AS`  
  
 `EXTERNAL NAME HelloWorld.Procedures.HelloWorld`  
  
 For more information about creating the different types of managed database objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [CLR User-Defined Functions](../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md), [CLR User-Defined Aggregates](../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates.md), [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md), [CLR Stored Procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures), and [CLR Triggers](/dotnet/framework/data/adonet/sql/clr-triggers).  
  
## Deploying the Assembly to Production Servers  
 Once the CLR database objects have been tested and verified on the test server, they can be distributed to production servers. For more information about debugging managed database objects, see [Debugging CLR Database Objects](../../relational-databases/clr-integration/debugging-clr-database-objects.md).  
  
 The deployment of managed database objects is similar to that of regular database objects (tables, [!INCLUDE[tsql](../../includes/tsql-md.md)] routines, and so on). The assemblies containing the CLR database objects can be deployed to other servers using a deployment script. The deployment script can be built by using the "Generate Scripts" functionality of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The deployment script can also be built manually, or built using "Generate Scripts" and manually altered. Once the deployment script has been built, it can be run on other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to deploy the managed database objects.  
  
#### To generate a deployment script using generate scripts  
  
1.  Open [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where the managed assembly or database object to be deployed is registered.  
  
2.  In the **Object Explorer**, expand the **\<server name>** and **Databases** trees. Right-click the database where the managed database object is registered, select **Tasks**, and then select **Generate Scripts**. The Script Wizard opens.  
  
3.  Select the database from the list box and click **Next**.  
  
4.  In the **Choose Script Options** pane, click **Next**, or change the options and then click **Next**.  
  
5.  In the **Choose Object Types** pane, choose the type of database object to be deployed. Click **Next**.  
  
6.  For every object type selected in the **Choose Object Types** pane, a **Choose \<type>** pane is presented. In this pane, you can choose from all the instances of that database object type registered in the specified database. Select one or more objects and click **Next**.  
  
7.  The **Output Options** pane comes up when all of the desired database object types have been selected. Select **Script to file** and specify a file path for the script. Select **Next**. Review your selections and click **Finish**. The deployment script is saved to the specified file path.  
  
## Post Deployment Scripts  
 You can run a post deployment script.  
  
 To add a post deployment script, add a file called postdeployscript.sql in your Visual Studio project directory. For example, right click your project in **Solution Explorer** and select **Add Existing Item**. Add the file in the root of the project, rather than in the Test Scripts folder.  
  
 When you click deploy, Visual Studio will run this script after the deployment of your project.  
  
## See Also  
 [Common Language Runtime &#40;CLR&#41; Integration Programming Concepts](../../relational-databases/clr-integration/common-language-runtime-clr-integration-programming-concepts.md)  
  
