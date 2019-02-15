---
title: "Step 2: Add and configure the Foreach Loop container | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 88a973cc-0f23-4ecf-adb6-5b06279c2df6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 2-2: Add and configure the Foreach Loop container

In this task, you add the ability to loop through a folder of flat files and apply Lesson 1's data flow transformation to each of those flat files. You do this by adding and configuring a Foreach Loop container to the control flow.  
  
The Foreach Loop container that you add must be able to connect to each flat file in the folder. Because all the files in the folder have the same format, the Foreach Loop container can use the same Flat File connection manager to connect to each of these files. The Flat File connection manager that the container uses is the one you created in Lesson 1.  
  
Currently, the Flat File connection manager from Lesson 1 connects to only one specific flat file. To iteratively connect to each flat file in the folder, you have to configure both the Foreach Loop container and the Flat File connection manager as follows:  
  
-   **Foreach Loop container:** You map the enumerated value of the container to a user-defined package variable. The container then uses this variable to dynamically modify the **ConnectionString** property of the Flat File connection manager and iteratively connect to each flat file in the folder.  
  
-   **Flat File connection manager:** You modify the connection manager that was created in Lesson 1 by using a user-defined variable to populate the connection manager's **ConnectionString** property.  
  
The procedures in this task show you how to create and modify the Foreach Loop container to use a user-defined package variable, and to add the data flow task into the loop. You'll learn how to modify the Flat File connection manager to use that user-defined variable in the next task.  
  
After you have made these modifications to the package, when the package is run, the Foreach Loop Container iterates through all files in the Sample Data folder. Each time a file is found that matches the criterion, the Foreach Loop Container populates the new variable with the file name, maps that variable to the **ConnectionString** property of the Sample Currency Data Flat File connection manager, and then runs the data flow against that file. In this way, each iteration of the Foreach Loop the Data Flow task consumes a different flat file.  
  
> [!NOTE]  
> Because [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] separates control flow from data flow, any looping that you add to the control flow won't require modification to the data flow. Therefore, the Lesson 1 data flow does not have to be changed.  
  
## Add a Foreach Loop container  
  
1.  In **SQL Server Data Tools**, select the **Control Flow** tab.  
  
2.  In the **SSIS Toolbox**, expand **Containers**, and then drag a **Foreach Loop Container** onto the design surface of the **Control Flow** tab.  
  
3.  Right-click the new **Foreach Loop Container** and select **Edit**.  
  
4.  In the **Foreach Loop Editor** dialog, on the **General** page, for **Name**, enter **Foreach File in Folder**. Select **OK**.  
  
5.  Right-click the Foreach Loop container, select **Properties**, and in the **Properties** window verify that the **LocaleID** property is set to **English (United States)**.  
  
## Configure the enumerator for the Foreach Loop container  
  
1.  Double-click **Foreach File in Folder** to reopen the **Foreach Loop Editor**.  
  
2.  Select **Collection**.  
  
3.  On the **Collection** page, select **Foreach File Enumerator**.  
  
4.  In the **Enumerator configuration** group, select **Browse**.  
  
5.  In the **Browse for Folder** dialog box, locate the folder on your machine that contains the Currency_*.txt files included with the sample data.

6.  In the **Files** box, enter **Currency_\*.txt**.  
  
## Map the enumerator to a user-defined variable  
  
1.  Select **Variable Mappings**.  
  
2.  On the **Variable Mappings** page, in the **Variable** column, select the empty cell and select **\<New Variable...>**.  
  
3.  In the **Add Variable** dialog box, for **Name** enter **varFileName**.  
  
    > [!NOTE]  
    > Variable names are case-sensitive.  
  
4.  Select **OK**.  
  
5.  Select **OK** again to exit the **Foreach Loop Editor** dialog.  
  
## Add the data flow task to the loop  
  
-   Drag the **Extract Sample Currency Data** data flow task onto the **Foreach File in Folder** Foreach Loop container.  
  
## Go to next task  
[Step 3: Modify the Flat File connection manager](../integration-services/lesson-2-3-modifying-the-flat-file-connection-manager.md)  
  
## See also  
[Configure a Foreach Loop container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25)  
[Use variables in packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)  
  
  
  
