---
title: "Installing R Components without Internet Access | Microsoft Docs"
ms.custom: ""
ms.date: "2017-02-10"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0a90c438-d78b-47be-ac05-479de64378b2
caps.latest.revision: 30
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Installing R Components without Internet Access
This topic describes how to get the R component setup files required for an offline installation of SQL Server R Services, and how to install them as part of an interactive SQL Server setup.

Typically, setup of the R components used in SQL Server R Services requires an Internet connection. Because some components and libraries are open source, Microsoft does not install them by default, but provides the related installers as a convenience on the Microsoft Download Center and other trusted sites.

> [!TIP]
> 
> For a step-by-step walkthrough of the offline installation process, with screenshots, see this article by the [SQL Server Customer Advisory Team](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/). It also covers patching and slipstream setup scenarios.
  

## 1. Obtain R Component Installation Files

There are two installers for R components: one for Microsoft R Open (SRO), and another for the Microsoft R Server and database components (SRS). You must download and install both to use SQL Server R Services. The SQL Server setup wizard will ensure that they are installed in the correct order.

  
1. Download the installers from the [Microsoft Download Center sites](#installerlocs) onto a computer with Internet access, and save the installer rather than running it. Be sure to get the files that match the version of SQL Server you will be installing.
2. Copy the installer (CAB) files to the computer where R Services is to be installed.  
3. If you are installing a language other than the default, modify the installer file names as described here: [Modifications Required for Different Language Locales](#modslocales).
4. Optionally, you can also download the archived source code for the open source components. For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).

## 2. Install R Services Offline using the SQL Server Setup wizard  

5. Run the SQL Server setup wizard.
6. When the setup wizard displays the Microsoft R Open licensing page, click  **Accept**.  
7. A dialog box opens that prompts you for the **Install Path** of the packages for Microsoft R Open and Microsoft R Server.
Enter the path to the location of the files you downloaded. 
8. click **Browse** to locate the folder containing the installer files you copied earlier, .
9. If the correct files are found, you can click **Next** to indicate that the components are available.
10. Complete the SQL Server setup wizard.
11. Perform the post-installation steps described in this topic: [Set Up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)

## <a name="installerlocs"></a>Links to R Component Installers

Release  |Download link  
---------|---------
**SQL Server 2016 RTM**     |           
Microsoft R Open     |[SRO_3.2.2.803_1033.cab](https://go.microsoft.com/fwlink/?LinkId=761266)     
Microsoft R Server     |[SRS_8.0.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=735051)      
**SQL Server 2016 CU 1**     |           
Microsoft R Open     |[SRO_3.2.2.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808803)     
Microsoft R Server     |[SRS_8.0.3.10000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=808805)      
**SQL Server 2016 CU 2**     |           
Microsoft R Open     |[SRO_3.2.2.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827398)     
Microsoft R Server     |[SRS_8.0.3.12000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=827399)  
**SQL Server 2016 CU 3**     |           
Microsoft R Open     |[SRO_3.2.2.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831785)     
Microsoft R Server     |[SRS_8.0.3.13000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=831676)  |
**SQL Server 2016 SP 1**     |           
Microsoft R Open     |[SRO_3.2.2.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824879)     
Microsoft R Server     |[SRS_8.0.3.15000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=824881)  
**SQL Server 2016 SP 1 GDR**     |           
Microsoft R Open     |[SRO_3.2.2.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)     
Microsoft R Server     |[SRS_8.0.3.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)  
**SQL Server vNext CTP 1**     |           
Microsoft R Open     |[SRO_3.3.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836819)     
Microsoft R Server     |[SRS_9.0.0.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=836818)  
**SQL Server vNext CTP 1.1** |           
Microsoft R Open     |[SRO_3.3.2.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834568)     
Microsoft R Server     |[SRS_9.0.1.16000_1033.cab](https://go.microsoft.com/fwlink/?LinkId=834567)  
  
 
## <a name="modslocales"></a>Modifications Required for Different Language Locales

If you download the .cab files as part of SQL Server setup on a computer with Internet access, the setup wizard detects the local language and automatically changes the language of the installer. 

However, if you are installing one of the localized versions of SQL Server to a computer without Internet access and download the R installers to a local share, you must manually edit the name of the downloaded files and insert the correct language identifier for the language you are installing. 

 
For example, if you are installing the Japanese version of SQL Server, you would change the name of the file from SRS_8.0.3.0_**1033**.cab to SRS_8.0.3.0_**1041**.cab.    
 
### Additional Prerequisites

Depending on your environment, you might need to make local copies of installers for the following prerequisites.  


Component  |Version   
---------|---------
[Microsoft AS OLE DB Provider for SQL Server 2016](https://go.microsoft.com/fwlink/?linkid=834405)     |  13.0.1601.5         
[Microsoft .NET Core](https://go.microsoft.com/fwlink/?linkid=834319)     | 1.0.1          
[Microsoft MPI](https://go.microsoft.com/fwlink/?linkid=834316)     | 7.1.12437.25          
[Microsoft Visual C++ 2013 Redistributable](https://go.microsoft.com/fwlink/?linkid=799853)     | 12.0.30501.0         
[Microsoft Visual C++ 2015 Redistributable](https://go.microsoft.com/fwlink/?linkid=828641)     | 14.0.23026.0         

  


## Support for slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that the SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

+ In SQL Server 2016, you can start a slipstream upgrade in SQL Server Management Studio by clicking **Tools**, and selecting **Check for Updates**. You can also type **SETUP.EXE** from a command prompt to start the SQL Server setup utility.

+ If the server does not have Internet access, you must download the SQL Server installer, and then download matching versions of the R component installers **before** beginning the update process.  The R components are not included by default with SQL Server because these components include open source software that is provided under a separate license. 

If you are adding R Services to an instance that was previously installed, use the updated version of the SQL Server installer, and the corresponding updated version of the R components. When you specify that the R feature is to be installed, the installer will look for the matching version of the R CAB files. 

## Command-line arguments for offline unattended upgrades

When performing unattended setup, use the following command-line arguments to specify the locations of the installers:
- */UPDATESOURCE* to specify the location of the local file containing the SQL Server update installer  
- */MRCACHEDIRECTORY* to specify the folder containing the R component CAB files
- */IACCEPTROPENLICENSETERMS="True"* to accept the separate R licensing agreement 

    
> [!TIP]
> For additional information about unattended and upgrade scenarios, including sample scripts, see this blog by the R Services Support team: [Deploying R Services on Computers without Internet Access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/).

## See Also  
 [Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)   
 [Troubleshooting R Services Setup](http://msdn.microsoft.com/library/ce6b902b-a4fa-4b0a-ac0d-be47a59c2a78)  
  
  