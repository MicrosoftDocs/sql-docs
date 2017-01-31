---
$title: "MarshalOptions Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "MarshalOptions property [ADO], VJ++ example"
ms.assetid: 9475c5f9-3a63-42cb-818c-4268e938a25c
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# MarshalOptions Property Example (VJ++)
This example uses the [MarshalOptions](../../../ado/reference/ado-api/marshaloptions-property-ado.md) property to specify what rows are sent back to the server—All Rows or only Modified Rows.  
  
```  
// BeginMarshalOptionsJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class MarshalOptionsX  
{  
   // The main entry point for the application.  
   public static void main (String[] args)  
   {  
      MarshalOptionsX();  
      System.exit(0);  
   }  
   // MarshalX Function  
  
   static void MarshalOptionsX()  
   {  
      // Define ADO Objects  
      Recordset rstEmployees = null;  
  
      // Declarations  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      String line = null;  
  
      try  
      {  
  
         // Open Recordset with names from Employees Table.  
         String strCnn = " Provider='sqloledb';Data Source='MySqlServer';" +  
                "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
         rstEmployees = new Recordset();  
         rstEmployees.setCursorType(AdoEnums.CursorType.KEYSET);  
         rstEmployees.setLockType(AdoEnums.LockType.OPTIMISTIC);  
         rstEmployees.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
  
         rstEmployees.open(  
            "SELECT fname,lname from Employee ORDER BY lname",   
            strCnn,AdoEnums.CursorType.KEYSET,   
            AdoEnums.LockType.OPTIMISTIC, AdoEnums.CommandType.TEXT);  
  
         // Store original data  
         String strOldFirst = rstEmployees.getField("fname").getString();  
         String strOldLast = rstEmployees.getField("lname").getString();  
  
         // Change data in edit buffer  
         rstEmployees.getField("fname").setString("Linda");  
         rstEmployees.getField("lname").setString("Kobara");  
  
         // Show contents of buffer and get user input  
         String strMessage = "Edit in progress: "+ "\n"+  
                   "Original Data  = \t"+ strOldFirst +" "+  
                   strOldLast + "\n" + "Data in Buffer = \t"+  
                   rstEmployees.getField("fname").getString()+ " " +  
                   rstEmployees.getField("lname").getString()+"\n"+"\n"+  
                   "Use Update to replace the original data with " +  
                   "the buffered data in the recordset";  
         String strMarshalAll = "Would you like to send all the rows" +  
                     " in the recordset back to the server";  
         String strMarshalModified = "Would you like to send only "+  
                         " modified rows back to the server";  
  
         System.out.println(strMessage + "\nEnter (Y/N)...");  
  
         if (in.readLine().equalsIgnoreCase("Y"))  
         {  
            System.out.println(strMarshalAll);  
            System.out.println("\nEnter (Y/N)...");  
  
            if(in.readLine().equalsIgnoreCase("Y"))  
            {  
               rstEmployees.setMarshalOptions(AdoEnums.MarshalOptions.ALL);  
               rstEmployees.update();  
            }  
            else  
            {  
               System.out.println(strMarshalModified);  
               System.out.println("\nEnter (Y/N)...");  
  
               if (in.readLine().equalsIgnoreCase("Y"))  
               {  
                  rstEmployees.setMarshalOptions(  
                     AdoEnums.MarshalOptions.MODIFIEDONLY);  
                  rstEmployees.update();  
               }  
            }  
  
         }  
  
         // Show the resulting data  
         System.out.println("\nData in recordset = " +   
            rstEmployees.getField("fname").getString() +   
            " " + rstEmployees.getField("lname").getString());  
  
         // Restore original data because this is a demonstration  
         if (!((strOldFirst.equals(rstEmployees.getField("fname")))  
            &&(strOldLast.equals(rstEmployees.getField("lname")))))  
         {  
            rstEmployees.getField("fname").setString(strOldFirst);  
            rstEmployees.getField("lname").setString(strOldLast);  
            rstEmployees.update();  
         }  
  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
      }  
  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO  
  
         // As passing a connection, check for null pointer first  
         if(rstEmployees!= null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getLocalizedMessage());  
         }  
      }  
  
      // System Read requires this catch  
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
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
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
// EndMarshalOptionsJ  
  
```  
  
## See Also  
 [MarshalOptions Property (ADO)](../../../ado/reference/ado-api/marshaloptions-property-ado.md)