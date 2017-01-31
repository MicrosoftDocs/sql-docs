---
$title: "AbsolutePage, PageCount, and PageSize Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "PageSize property [ADO], VJ++ example"
  - "AbsolutePage property [ADO], VJ++ example"
  - "PageCount property [ADO], VJ++ example"
ms.assetid: 05f9f20e-0697-46bf-b004-76d7fc2e5d52
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# AbsolutePage, PageCount, and PageSize Properties Example (VJ++)
This example uses the [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md), [PageCount](../../../ado/reference/ado-api/pagecount-property-ado.md), and [PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md) properties to display names and hire dates from the ***Employees*** table, five records at a time.  
  
```  
// BeginAbsolutePageJ  
// The WFC class includes the ADO objects.  
  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class AbsolutePageX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      AbsolutePageX();  
      System.exit(0);  
   }  
  
   // AbsolutePageX function  
  
   static void AbsolutePageX()  
   {  
      // Define ADO Objects.  
      Recordset rstEmployees = null;  
  
      // Declarations.  
      BufferedReader in = new BufferedReader (new   
         InputStreamReader(System.in));  
      String line = null;  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      String strName;  
      String strFName;  
      String strLName;  
      String strHDate;  
      int intPage;  
      int intRecord;  
  
      try  
      {  
         rstEmployees = new Recordset();  
  
         // Use client cursor to enable AbsolutePosition property.  
         rstEmployees.setCursorLocation( AdoEnums.CursorLocation.CLIENT);  
  
         // Open a recordset using client cursor for the Employees table.  
         rstEmployees.open("employee", strCnn,  
            AdoEnums.CursorType.FORWARDONLY,  
            AdoEnums.LockType.READONLY,  
            AdoEnums.CommandType.TABLE);  
  
         // Display names and hire dates, five records at a time.  
  
         rstEmployees.setPageSize(5);  
         int intPageCount = rstEmployees.getPageCount();  
         for ( intPage = 1; intPage <= intPageCount; intPage++)  
         {  
            strName = "";  
            rstEmployees.setAbsolutePage(intPage);  
            for ( intRecord = 1; intRecord <= rstEmployees.getPageSize();   
               intRecord++)  
            {  
               strFName = rstEmployees.getField("fname").getString();  
               strLName = rstEmployees.getField("lname").getString();  
               strHDate = rstEmployees.getField("hire_date").getString();  
  
               strHDate = strHDate.substring(5,7) + "/" +   
                  strHDate.substring(8,10) +  
                  "/" + strHDate.substring(2,4);  
  
               strName = strName + "\n" + strFName + " " + strLName +   
                  "  " + strHDate;  
               rstEmployees.moveNext();  
               if ( rstEmployees.getEOF())  
                  break;  
            }  
            System.out.println(strName);  
            // Get user input to display next records.  
  
            System.out.println("\n\nPress <Enter> key to Continue.");  
            line = in.readLine();  
         }  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if (rstEmployees.getActiveConnection()==null)  
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
// EndAbsolutePageJ  
```  
  
## See Also  
 [AbsolutePage Property (ADO)](../../../ado/reference/ado-api/absolutepage-property-ado.md)   
 [PageCount Property (ADO)](../../../ado/reference/ado-api/pagecount-property-ado.md)   
 [PageSize Property (ADO)](../../../ado/reference/ado-api/pagesize-property-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)