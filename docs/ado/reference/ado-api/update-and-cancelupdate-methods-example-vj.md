---
title: "Update and CancelUpdate Methods Example (VJ++) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "CancelUpdate method [ADO]"
  - "Update method [ADO], VJ++ example"
ms.assetid: f93099ae-797d-4f0d-ac28-81405b2892e1
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Update and CancelUpdate Methods Example (VJ++)
This example demonstrates the [Update](../../../ado/reference/ado-api/update-method.md) method in conjunction with the [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method.  
  
```  
// BeginUpdateJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class UpdateX  
{  
   // The main entry point of the application.  
  
   public static void main (String[] args)  
   {  
      UpdateX();  
      UpdateX2();  
      System.exit(0);  
   }  
   // UpdateX Function  
  
   static void UpdateX()  
   {  
      // Define ADO objects.  
      Recordset rstEmployees = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"+  
                  "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strOldFirst;  
      String strOldLast;  
      String strMessage;  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         // Open Recordset with names from Employees table.  
         rstEmployees = new Recordset();  
         rstEmployees.setCursorType(AdoEnums.CursorType.KEYSET);  
         rstEmployees.setLockType(AdoEnums.LockType.OPTIMISTIC);  
         rstEmployees.open("SELECT fname,lname FROM Employees " +  
            "ORDER BY lname", strCnn, AdoEnums.CursorType.KEYSET,   
            AdoEnums.LockType.OPTIMISTIC, AdoEnums.CommandType.TEXT);  
  
         // Store Original data.  
         strOldFirst = rstEmployees.getField("fname").getString();  
         strOldLast = rstEmployees.getField("lname").getString();  
  
         // Change data in edit buffer.  
         rstEmployees.getField("fname").setString("Linda");  
         rstEmployees.getField("lname").setString("Kobara");  
  
         // Show contents of buffer and get user input.  
         strMessage = "Edit in Progress :\n" +  
            "\tOriginal Data = " + strOldFirst + "  " + strOldLast +  
            "\n\tData in Buffer = " +   
            rstEmployees.getField("fname").getString() + " " +   
            rstEmployees.getField("lname").getString() + "\n\n" +  
             "Use Update to replace the original data with " +  
            "the buffered data in the Recordset?Enter (Y/N)";  
  
         System.out.println(strMessage);  
         if(in.readLine().trim().equalsIgnoreCase("Y"))  
            rstEmployees.update();  
         else  
            rstEmployees.cancelUpdate();  
  
         // Show the resulting data.  
         System.out.println("Data in Recordset = " +   
            rstEmployees.getField("fname").getString() +  
            "  " + rstEmployees.getField("lname").getString()+ "\n");  
  
         // Restore original data because this is a demonstration.  
         if(!(strOldFirst.equals(  
            rstEmployees.getField("fname").getString()) &&   
            strOldLast.equals(  
            rstEmployees.getField("lname").getString())))  
         {  
            rstEmployees.getField("fname").setString(strOldFirst);  
            rstEmployees.getField("lname").setString(strOldLast);  
            rstEmployees.update();  
         }  
         // Cleanup Objects before exit.  
         rstEmployees.close();  
  
         System.out.println("Press <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a recordset, check for null pointer first.  
         if(rstEmployees != null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: "+ ae.getMessage());  
         }  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
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
   // UpdateX2 Function  
  
   static void UpdateX2()  
   {  
      // This example demonstrates the Update method in conjunction  
      // with the AddNew method.  
  
      // Define ADO Objects.  
      Connection cnn1 = null;  
      Recordset rstEmployees = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"+  
                  "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strEmpID;  
      String strOldFirst;  
      String strOldLast;  
      String strMessage;  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         // Open a connection.  
         cnn1 = new Connection();  
         cnn1.open(strCnn);  
  
         // Open Recordset with data from Employees table.  
         rstEmployees = new Recordset();  
         rstEmployees.setCursorType(AdoEnums.CursorType.KEYSET);  
         rstEmployees.setLockType(AdoEnums.LockType.OPTIMISTIC);  
         rstEmployees.open("employee", cnn1, AdoEnums.CursorType.KEYSET,   
            AdoEnums.LockType.OPTIMISTIC, AdoEnums.CommandType.TABLE);  
  
         rstEmployees.addNew();  
         strEmpID="B-S55555M";  
         rstEmployees.getField("emp_id").setString(strEmpID);  
         rstEmployees.getField("fname").setString("Bill");  
         rstEmployees.getField("lname").setString("Sornsin");  
  
         // Show contents of buffer and get user input.  
         strMessage = "AddNew in progress : " + "\n" +  
            "\tData in Buffer = " +   
            rstEmployees.getField("emp_id").getString() +  
            "  " + rstEmployees.getField("fname").getString() + "  " +  
            rstEmployees.getField("lname").getString()+ "\n\n" +  
            "Use Update to save buffer to recordset?Enter (Y/N)";  
         System.out.println(strMessage);  
         if(in.readLine().trim().equalsIgnoreCase("Y"))  
         {  
            rstEmployees.update();  
            // Go to the new record and show the resulting data.  
            System.out.println("Data in recordset = " +   
               rstEmployees.getField("emp_id").getString()+  
               "  " + rstEmployees.getField("fname").getString() +  
               "  " + rstEmployees.getField("lname").getString() + "\n");  
         }  
         else  
         {  
            rstEmployees.cancelUpdate();  
            System.out.println("No new Record added.\n");  
         }  
         // Delete new data because this is a demonstration.  
         cnn1.execute(  
            "DELETE FROM employee WHERE emp_id='" + strEmpID +"'");  
  
         // Cleanup Objects before exit  
         rstEmployees.close();  
  
         System.out.println("Press <Enter> to continue..");  
         in.readLine();  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a recordset, check for null pointer first.  
         if(rstEmployees != null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: "+ ae.getMessage());  
         }  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
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
   // PrintProviderError Function  
   static void PrintProviderError(Connection cnn1)  
   {  
      // Print Provider Errors from Connection Object.  
      // ErrItem is an item object in the Connections Errors Collection.  
      com.ms.wfc.data.Error               ErrItem = null;  
      long                                 nCount = 0;  
      int                                       i = 0;  
  
      nCount = cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if ( nCount > 0)  
      {  
         // Collection ranges from 0 to nCount-1.  
         for ( i=0;i<nCount; i++)  
         {  
            ErrItem = cnn1.getErrors().getItem(i);  
            System.out.println("\t Error Number: " + ErrItem.getNumber()   
               + "\t" + ErrItem.getDescription());  
         }  
      }  
   }  
   // PrintIOError Function  
   static void PrintIOError(java.io.IOException je)  
   {  
      System.out.println("Error: \n");  
      System.out.println("\t Source: " + je.getClass() + "\n");  
      System.out.println("\t Description: "+ je.getMessage() + "\n");  
   }  
}  
// EndUpdateJ  
  
```  
  
## See Also  
 [CancelUpdate Method (ADO)](../../../ado/reference/ado-api/cancelupdate-method-ado.md)   
 [Update Method](../../../ado/reference/ado-api/update-method.md)