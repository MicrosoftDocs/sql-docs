---
title: "Step 2: Adding and Configuring the Foreach Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 88a973cc-0f23-4ecf-adb6-5b06279c2df6
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 2: Adding and Configuring the Foreach Loop Container
  In this task, you will add the ability to loop through a folder of flat files and apply the same data flow transformation used in Lesson 1 to each of those flat files. You do this by adding and configuring a Foreach Loop container to the control flow.  
  
 The Foreach Loop container that you add must be able to connect to each flat file in the folder. Because all the files in the folder have the same format, the Foreach Loop container can use the same Flat File connection manager to connect to each of these files. The Flat File connection manager that the container will use is the same Flat File connection manager that you created in Lesson 1.  
  
 Currently, the Flat File connection manager from Lesson 1 connects to only one, specific flat file. To iteratively connect to each flat file in the folder, you will have to configure both the Foreach Loop container and the Flat File connection manager as follows:  
  
-   **Foreach Loop container:** You will map the enumerated value of the container to a user-defined package variable. The container will then use this user-defined variable to dynamically modify the `ConnectionString` property of the Flat File connection manager and iteratively connect to each flat file in the folder.  
  
-   **Flat File connection manager:** You will modify the connection manager that was created in Lesson 1 by using a user-defined variable to populate the connection manager's `ConnectionString` property.  
  
 The procedures in this task show you how to create and modify the Foreach Loop container to use a user-defined package variable and to add the data flow task to the loop. You will learn how to modify the Flat File connection manager to use a user-defined variable in the next task.  
  
 After you have made these modifications to the package, when the package is run, the Foreach Loop Container will iterate through the collection of files in the Sample Data folder. Each time a file is found that matches the criteria, the Foreach Loop Container will populate the user-defined variable with the file name, map the user-defined variable to the `ConnectionString` property of the Sample Currency Data Flat File connection manager, and then run the data flow against that file. Therefore, in each iteration of the Foreach Loop the Data Flow task will consume a different flat file.  
  
> [!NOTE]  
>  Because [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] separates control flow from data flow, any looping that you add to the control flow will not require modification to the data flow. Therefore, the data flow that you created in Lesson 1 does not have to be changed.  
  
### To add a Foreach Loop container  
  
1.  In **SQL Server Data Tools**, click the **Control Flow** tab.  
  
2.  In the **SSIS Toolbox**, expand **Containers**, and then drag a **Foreach Loop Container** onto the design surface of the **Control Flow** tab.  
  
3.  Right-click the newly added **Foreach Loop Container** and select **Edit**.  
  
4.  In the **Foreach Loop Editor** dialog box, on the **General** page, for **Name**, enter `Foreach File in Folder`. Click **OK**.  
  
5.  Right-click the Foreach Loop container, click **Properties**, and in the Properties window, verify that the `LocaleID` property is set to **English (United States)**.  
  
### To configure the enumerator for the Foreach Loop container  
  
1.  Double-click Foreach File in Folder to reopen the **Foreach Loop Editor**.  
  
2.  Click **Collection**.  
  
3.  On the **Collection** page, select **Foreach File Enumerator**.  
  
4.  In the **Enumerator configuration** group, click **Browse**.  
  
5.  In the **Browse for Folder** dialog box, locate the folder on your machine that contains the Currency_*.txt files.  
  
     This sample data is included with the [!INCLUDE[ssIS](../includes/ssis-md.md)] lesson packages. To download the sample data and the lesson packages, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=275027)  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the  HYPERLINK "http://msftisprodsamples.codeplex.com/downloads/get/578097" SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
6.  In the **Files** box, type **Currency_\*.txt**.  
  
### To map the enumerator to a user-defined variable  
  
1.  Click **Variable Mappings**.  
  
2.  On the **Variable Mappings** page, in the **Variable** column, click the empty cell and select **\<New Variable...>**.  
  
3.  In the **Add Variable** dialog box, for **Name**, type `varFileName`.  
  
    > [!IMPORTANT]  
    >  Variable names are case sensitive.  
  
4.  Click **OK**.  
  
5.  Click **OK** again to exit the **Foreach Loop Editor** dialog box.  
  
### To add the data flow task to the loop  
  
-   Drag the **Extract Sample Currency Data** data flow task onto the Foreach Loop container now renamed `Foreach File in Folder`.  
  
## Next Lesson Task  
 [Step 3: Modifying the Flat File Connection Manager](lesson-2-3-modifying-the-flat-file-connection-manager.md)  
  
## See Also  
 [Configure a Foreach Loop Container](control-flow/foreach-loop-container.md)   
 [Use Variables in Packages](use-variables-in-packages.md)  
  
  
