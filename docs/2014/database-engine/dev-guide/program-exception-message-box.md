---
title: "Program Exception Message Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "database-engine"
ms.topic: "reference"
helpviewer_keywords: 
  - "exception message box [SQL Server]"
  - "displaying exception message box"
ms.assetid: c771985b-149c-459a-b3cb-7b15fde01150
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Program Exception Message Box
  You can use the exception message box in your applications to provide significantly more control over the messaging experience than is provided by the <xref:System.Windows.Forms.MessageBox> class. For more information, see [Exception Message Box Programming](../../../2014/database-engine/dev-guide/exception-message-box-programming.md). For information about how to obtain and deploy the exception message box .dll, see [Deploying an Exception Message Box Application](../../../2014/database-engine/dev-guide/deploying-an-exception-message-box-application.md).  
  
## Procedure  
  
#### To handle an exception by using the exception message box  
  
1.  Add a reference in your managed code project to the Microsoft.ExceptionMessageBox.dll assembly.  
  
2.  (Optional) Add a `using` (C#) or `Imports` ([!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic .NET) directive to use the <xref:Microsoft.SqlServer.MessageBox> namespace.  
  
3.  Create a try-catch block to handle the anticipated exception.  
  
4.  Within the `catch` block, create an instance of the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox> class. Pass the <xref:System.Exception> object handled by the `try`-`catch` block.  
  
5.  (Optional) Set one or more of the following properties on <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox>:  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Buttons%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons> enumeration that specifies the buttons to display in the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.DefaultButton%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxDefaultButton> enumeration that specifies the default button for the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Options%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxOptions> enumeration that you use to control other behaviors of the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Symbol%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxSymbol> enumeration that specifies the symbol to display in the exception message box.  
  
6.  Call the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Show%2A> method. Pass the parent window to which the exception message box belongs.  
  
7.  (Optional) Note the value of returned <xref:System.Windows.Forms.DialogResult> enumeration if you need to determine which button the user clicked.  
  
#### To display the exception message box without an exception  
  
1.  Add a reference in your managed code project to the Microsoft.ExceptionMessageBox.dll assembly.  
  
2.  (Optional) Add a `using` (C#) or `Imports` (Visual Basic .NET) directive to use the <xref:Microsoft.SqlServer.MessageBox> namespace.  
  
3.  Create an instance of the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox> class. Pass the message text as a <xref:System.String> value.  
  
4.  (Optional) Set one or more of the following properties on <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox>:  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Buttons%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons> enumeration that specifies the buttons to display in the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Caption%2A> - Dialog box caption of the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.DefaultButton%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxDefaultButton> enumeration that specifies the default button for the dialog box of the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Options%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxOptions> enumeration that you use to control other behaviors of the exception message box.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Symbol%2A> - <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxSymbol> enumeration that specifies the symbol to display in the exception message box.  
  
5.  Call the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Show%2A> method. Pass the parent window to which the exception message box belongs.  
  
6.  (Optional) Note the value of the returned <xref:System.Windows.Forms.DialogResult> enumeration if you need to determine which button the user clicked.  
  
#### To display the exception message box with customized buttons  
  
1.  Add a reference in your managed code project to the Microsoft.ExceptionMessageBox.dll assembly.  
  
2.  (Optional) Add a `using` (C#) or `Imports` (Visual Basic .NET) directive to use the <xref:Microsoft.SqlServer.MessageBox> namespace.  
  
3.  Create an instance of the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox> class in one of two ways:  
  
    -   Pass the <xref:System.Exception> object handled by a `try`-`catch` block.  
  
    -   Pass the message text as a <xref:System.String> value.  
  
4.  Set one of the following values for <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Buttons%2A>:  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.AbortRetryIgnore> - displays the **Abort**, **Retry**, and **Ignore** buttons.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.Custom> - displays custom buttons.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.OK> - displays the **OK** button.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.OKCancel> - displays the **OK** and **Cancel** buttons.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.RetryCancel> - displays the **Retry** and **Cancel** buttons.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.YesNo> - displays **Yes** and **No** buttons.  
  
    -   <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxButtons.YesNoCancel> - displays **Yes**, **No**, and **Cancel** buttons.  
  
5.  (Optional) If you use custom buttons, call one of the overloads of the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.SetButtonText%2A> method to specify text for up to five custom buttons.  
  
6.  Call the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Show%2A> method. Pass the parent window to which the exception message box belongs.  
  
7.  (Optional) Note the value of the returned <xref:System.Windows.Forms.DialogResult> enumeration if you need to determine which button the user clicked. If you use custom buttons, note the value of <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBoxDialogResult> for the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.CustomDialogResult%2A> property to determine which of the custom buttons the user clicked.  
  
#### To allow users to decide whether to show the exception message box  
  
1.  Add a reference in your managed code project to the Microsoft.ExceptionMessageBox.dll assembly.  
  
2.  (Optional) Add a `using` (C#) or `Imports` (Visual Basic .NET) directive to use the <xref:Microsoft.SqlServer.MessageBox> namespace.  
  
3.  Create an instance of the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox> class in one of two ways:  
  
    -   Pass the <xref:System.Exception> object handled by a `try`-`catch` block.  
  
    -   Pass the message text as a <xref:System.String> value.  
  
4.  Set the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.ShowCheckbox%2A> property to `true`.  
  
5.  (Optional) Specify the text that asks the user to decide whether to show the exception message box again for <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.CheckboxText%2A>. The default text is "Do not show this message again."  
  
6.  If you need to keep the user's decision only for the duration of the application's execution, set the value of <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.IsCheckboxChecked%2A> to a global <xref:System.Boolean> variable. Evaluate this value before creating an instance of the exception message box.  
  
7.  If you need to store a user's decision permanently, do the following:  
  
    1.  Call the CreateSubKey method to open a custom registry key that your application uses, and set <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.CheckboxRegistryKey%2A> to the returned RegistryKey object.  
  
    2.  Set <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.CheckboxRegistryValue%2A> to the name of the registry value that is used.  
  
    3.  Set <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.CheckboxRegistryMeansDoNotShowDialog%2A> to `true`.  
  
    4.  Call the <xref:Microsoft.SqlServer.MessageBox.ExceptionMessageBox.Show%2A> method. The specified registry key is evaluated, and the exception message box is displayed only if the data stored in the registry key is 0. If the dialog box is displayed and the user selects the check box before clicking a button, the data in the registry key is set to 1.  
  
## Example  
 This example uses the exception message box with only the **OK** button to display information from an application exception that includes the handled exception along with additional application-specific information.  
  
 [!code-csharp[HowTo#emb_showOKbutton](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_showokbutton)]  
  
 [!code-vb[HowTo#emb_vb_showOKbutton](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_showokbutton)]  
  
## Example  
 This example uses the exception message box with **Yes** and **No** buttons from which the user chooses.  
  
 [!code-csharp[HowTo#emb_showYesNobutton](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_showyesnobutton)]  
  
 [!code-vb[HowTo#emb_vb_showYesNobutton](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_showyesnobutton)]  
  
## Example  
 This example uses the exception message box with custom buttons.  
  
 [!code-csharp[HowTo#emb_showcustombutton](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_showcustombutton)]  
  
 [!code-vb[HowTo#emb_vb_showcustombutton](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_showcustombutton)]  
  
## Example  
 This example uses the check box to determine whether to show the exception message box.  
  
 [!code-csharp[HowTo#emb_usecheckbox](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_usecheckbox)]  
  
 [!code-vb[HowTo#emb_vb_usecheckbox](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_usecheckbox)]  
  
## Example  
 This example uses the check box and a registry key to determine whether to show the exception message box.  
  
 [!code-csharp[HowTo#emb_useregkey](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_useregkey)]  
  
 [!code-vb[HowTo#emb_vb_useregkey](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_useregkey)]  
  
## Example  
 This example uses the exception message box to show additional information that is helpful when troubleshooting or debugging.  
  
 [!code-csharp[HowTo#emb_moreinfo](../../snippets/csharp/SQL15/replication/howto/cs/embform.cs#emb_moreinfo)]  
  
 [!code-vb[HowTo#emb_vb_moreinfo](../../snippets/visualbasic/SQL15/replication/howto/vb/embform.vb#emb_vb_moreinfo)]  
  
  
