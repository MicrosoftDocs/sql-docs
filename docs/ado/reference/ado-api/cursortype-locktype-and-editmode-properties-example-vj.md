---
title: "CursorType, LockType, and EditMode Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "LockType property [ADO], VJ++ example"
  - "EditMode property [ADO], VJ++ example"
  - "CursorType property [ADO], VJ++ example"
ms.assetid: c222016e-415d-485e-86c5-e29feac4297a
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# CursorType, LockType, and EditMode Properties Example (VJ++)
This example demonstrates setting the [CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md) and [LockType](../../../ado/reference/ado-api/locktype-property-ado.md) properties before opening a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). It also shows the value of the [EditMode](../../../ado/reference/ado-api/editmode-property.md) property under various conditions. The EditModeOutput function is required for this procedure to run.  
  
```  
// BeginEditModeJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class EditModeX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      EditModeX ();  
      System.exit(0);  
   }  
  
   // EditModeX function  
  
   static void EditModeX ()  
   {  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstEmployees = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
            + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      try  
      {  
         // Open recordset with data from Employees table.  
  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         rstEmployees = new Recordset();  
         rstEmployees.setActiveConnection(cnConn1);  
         rstEmployees.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
         rstEmployees.setCursorType(AdoEnums.CursorType.STATIC);  
         rstEmployees.setLockType(AdoEnums.LockType.BATCHOPTIMISTIC);  
         rstEmployees.open("employee", cnConn1,  
            AdoEnums.CursorType.STATIC,  
            AdoEnums.LockType.BATCHOPTIMISTIC,  
            AdoEnums.CommandType.TABLE);  
  
         // Show the EditMode property under different editing states.  
  
         rstEmployees.addNew();  
         rstEmployees.getField("emp_id").setString("T-T55555M");  
         rstEmployees.getField("fname").setString("temp_fname");  
         rstEmployees.getField("lname").setString("temp_lname");  
         EditModeOutput("After AddNew:", rstEmployees.getEditMode());  
         rstEmployees.updateBatch();  
         EditModeOutput("After Update:", rstEmployees.getEditMode());  
         rstEmployees.getField("fname").setString("test");  
         EditModeOutput("After Edit:", rstEmployees.getEditMode());  
  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
         // Delete new record because this is a demonstration.  
         cnConn1.execute(  
            "DELETE FROM employee WHERE emp_id = 'T-T55555M'");  
  
         // Cleanup objects before exit.  
         rstEmployees.close();  
         cnConn1.close();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if (cnConn1==null)  
            System.out.println("Exception: " + ae.getMessage());  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstEmployees != null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstEmployees != null)  
            if (rstEmployees.getState() == 1)  
               rstEmployees.close();  
      }  
   }  
  
   // EditModeOutput Function  
  
   static void EditModeOutput(String strTemp, int intEditMode)  
   {  
      String strMessage="";  
      // Print report based on the value of the EditMode  
      // property.  
  
      System.out.println("\n" + strTemp);  
      strMessage ="\n\tEditMode = ";  
      switch(intEditMode)  
      {  
      case AdoEnums.EditMode.NONE :  
         strMessage+="adEditNone";  
         break;  
      case AdoEnums.EditMode.INPROGRESS :  
         strMessage+="adEditInProgress";  
         break;  
      case AdoEnums.EditMode.ADD :  
         strMessage+="adEditAdd";  
         break;  
      default :  
         break;  
      }  
      System.out.println(strMessage);  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connection's Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndEditModeJ  
```  
  
## See Also  
 [CursorType Property (ADO)](../../../ado/reference/ado-api/cursortype-property-ado.md)   
 [EditMode Property](../../../ado/reference/ado-api/editmode-property.md)   
 [LockType Property (ADO)](../../../ado/reference/ado-api/locktype-property-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)