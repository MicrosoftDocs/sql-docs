---
title: "Step 3: Add error flow redirection | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 5683a45d-9e73-4cd5-83ca-fae8b26b488c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 4-3: Add error flow redirection

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



In the previous task, the Lookup Currency Key transformation cannot generate a match when the transformation tries to process the corrupted sample flat file, which produces an error. Because the transformation uses the default settings for error output, any error causes the transformation to fail. When the transformation fails, the rest of the package also fails.  
  
Rather than permitting the transformation to fail, you can configure the component to redirect the failed row to another processing path, by using the error output. Using a separate error processing path provides more options. For instance, you could clean the data and then reprocess the failed row. Or, you could save the failed row along with its error information for later verification and reprocessing.  
  
In this task, you configure the Lookup Currency Key transformation to redirect any rows that fail to the error output. In the error branch of the data flow, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] writes these rows to a file.  
  
By default the two extra columns in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] error output, **ErrorCode** and **ErrorColumn**, contain only a numeric error code and the ID of the column in which the error occurred. In this task, before the package writes the failed rows to the file, you use a Script component to access the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] API and get a description of the error.  
  
## Configure an error output  
  
1.  In the **SSIS Toolbox**, expand **Common**, and then drag **Script Component** onto the design surface of the **Data Flow** tab. Place **Script** to the right of the **Lookup Currency Key** transformation.  
  
2.  In the **Select Script Component Type** dialog box, select **Transformation**, and select **OK**.  
  
3.  To connect the two components, select the **Lookup Currency Key** transformation and then drag its red arrow onto the new **Script** transformation.  
  
    The red arrow represents the error output of the **Lookup Currency Key** transformation. By using the red arrow to connect the transformation to the Script component, you redirect any processing errors to the Script component, which processes the errors and sends them to the destination.  
  
4.  In the **Configure Error Output** dialog box, in the **Error** column, select **Redirect row**, and then select **OK**.  
  
5.  On the **Data Flow** design surface, select the name **Script Component** in the new **ScriptComponent**, and change that name to **Get Error Description**.  
  
6.  Double-click the **Get Error Description** transformation.  
  
7.  In the **Script Transformation Editor** dialog box, on the **Input Columns** page, select the **ErrorCode** column.  
  
8.  On the **Inputs and Outputs** page, expand **Output 0**, select **Output Columns**, and then select **Add Column**.  
  
9. In the **Name** property, enter *ErrorDescription* and set the **DataType** property to **Unicode string [DT_WSTR]**.  
  
10. On the **Script** page, verify that the **LocaleID** property is set to **English (United States)**.
  
11. Select **Edit Script** to open [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] Tools for Applications (VSTA). In the **Input0_ProcessInputRow** method, enter or paste the following code:  
  
    [Visual Basic]  
  
    ```vb  
    Row.ErrorDescription =   
      Me.ComponentMetaData.GetErrorDescription(Row.ErrorCode)  
    ```  
  
    [Visual C#]  
  
    ```cs
    Row.ErrorDescription = this.ComponentMetaData.GetErrorDescription(Row.ErrorCode);  
    ```  
  
    The completed subroutine looks like the following code:  
  
    [Visual Basic]  
  
    ```vb
    Public Overrides Sub Input0_ProcessInputRow(ByVal Row As Input0Buffer)  
  
      Row.ErrorDescription =   
        Me.ComponentMetaData.GetErrorDescription(Row.ErrorCode)  
  
    End Sub  
    ```  
  
    [Visual C#]  
  
    ```cs
    public override void Input0_ProcessInputRow(Input0Buffer Row)  
        {  
  
            Row.ErrorDescription = this.ComponentMetaData.GetErrorDescription(Row.ErrorCode);  
  
        }  
    ```  
  
12. On the **Build** menu, select **Build Solution** to build the script and save your changes, and then close VSTA.  
  
13. Select **OK** to close the **Script Transformation Editor** dialog box.  
  
## Go to next task
[Step 4: Add a Flat File destination](../integration-services/lesson-4-4-adding-a-flat-file-destination.md)  
  
  
  
