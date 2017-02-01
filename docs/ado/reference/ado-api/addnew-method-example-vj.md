---
title: "AddNew Method Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "AddNew method [ADO], VJ++ example"
ms.assetid: 12856ffb-8645-4fad-b51f-2c2967677c01
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# AddNew Method Example (VJ++)
This example uses the [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md) method to create a new record with the specified name.  
  
```  
// BeginAddNewJ  
import com.ms.wfc.data.*;  
import java.io.*;  
  
public class AddNewX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      AddNewX();  
      System.exit(0);  
   }  
  
   // AddNewX function  
  
   static void AddNewX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstEmployees = null;  
  
      //Declarations.  
      String strCnn;  
      String strID;  
      String strFirstName;  
      String strLastName;  
      boolean booRecordAdded ;  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader (System.in));  
      String line = null;  
  
      try  
      {  
         // Open a connection.  
         strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
            + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);//,"","",AdoEnums.CommandType.UNSPECIFIED);  
  
         // Open Employees table.  
         rstEmployees = new Recordset ();  
         rstEmployees.open("employee", cnConn1,   
            AdoEnums.CursorType.KEYSET , AdoEnums.LockType.OPTIMISTIC ,   
            AdoEnums.CommandType.TABLE );  
  
         /* Get data from the user. The employee ID must be formatted as  
         first,middle, and last initial, five numbers, then M or F to  
         signify the gender. For example, the employee id for Bill   
         Sornsin would be "B-S55555M". */  
         System.out.println("\nEnter employee ID :");  
         strID = in.readLine().trim();  
         System.out.println("\nEnter first name :");  
         strFirstName = in.readLine().trim();  
         System.out.println("\nEnter last name :");  
         strLastName = in.readLine().trim();  
  
         // Proceed only if the user actually entered something  
         // for both the first and last names.  
  
         if ( !(strID.compareTo("") == 0) &   
            !(strFirstName.compareTo("") == 0) &   
            !(strLastName.compareTo("")== 0))  
         {  
            // Add new record.  
            rstEmployees.addNew();  
            rstEmployees.getField("emp_id").setString(strID);  
            rstEmployees.getField("fname").setString(strFirstName);  
            rstEmployees.getField("lname").setString(strLastName);  
  
            // update the record with the new data.  
            rstEmployees.update();  
            booRecordAdded = true;  
  
            // Show the newly added data.  
            System.out.println("\nNew record : "   
               + rstEmployees.getField("emp_id").getString()+ " "  
               + rstEmployees.getField("fname").getString()+ " "  
               + rstEmployees.getField("lname").getString());  
         }  
         else  
         {  
            System.out.println("\nPlease enter an employee ID," +  
               "first name, and last name.");  
         }  
  
         System.out.println("\n\nPress <Enter> key to continue.");  
         line = in.readLine();  
  
         // Delete the new record because this is a demonstration.  
         cnConn1.execute("DELETE FROM employee WHERE "   
            + "emp_id = '" + strID + "'");  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         //Check for null pointer for connection object.  
         if(rstEmployees.getActiveConnection()==null)  
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
         System.exit(0);  
      }  
      // System Read requires this catch.  
      catch( java.io.IOException je )  
      {  
         PrintIOError(je);  
      }        
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstEmployees != null)  
            if (rstEmployees.getState() == 1)  
               rstEmployees.close();     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
         System.exit(0);  
      }  
  
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
  
   //.PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
  
// EndAddNewJ  
```  
  
## See Also  
 [AddNew Method (ADO)](../../../ado/reference/ado-api/addnew-method-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)