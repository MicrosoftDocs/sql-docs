---
$title: "Item Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Item property [ADO], VJ++ example"
ms.assetid: e1426a08-71b8-4af2-9f57-97ceb90ccef2
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Item Property Example (VJ++)
This example demonstrates how the [Item](../../../ado/reference/ado-api/item-property-ado.md) property accesses members of a collection. The example opens the ***Authors*** table of the ***Pubs*** database with a parameterized command.  
  
 The parameter in the command issued against the database is accessed from the [Command](../../../ado/reference/ado-api/command-object-ado.md) object's [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection by index and name. Then the fields of the returned [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) are accessed from that object's [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection by index and name.  
  
```  
// BeginItemJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
import com.ms.com.*;  
  
public class ItemX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      ItemX();  
      System.exit(0);  
   }  
  
   // ItemX  function  
  
   static void ItemX()  
   {  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstAuthors = null;  
      Command cmd = null;  
      Parameter prm = null;  
      Field fld = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
            "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      Variant [] varColumn = null;  
      int intIndex;  
      int intLimit;  
  
      try  
      {  
         cnConn1 = new Connection();  
         rstAuthors = new Recordset();  
         cmd = new Command();  
  
         //  Set the array with the Authors table field (column) names.  
         varColumn = new Variant[9];  
         varColumn[0] = new Variant("au_id");  
         varColumn[1] = new Variant("au_lname");  
         varColumn[2] = new Variant("au_fname");  
         varColumn[3] = new Variant("phone");  
         varColumn[4] = new Variant("address");  
         varColumn[5] = new Variant("city");  
         varColumn[6] = new Variant("state");  
         varColumn[7] = new Variant("zip");  
         varColumn[8] = new Variant("contract");  
  
         cmd.setCommandText("SELECT * FROM Authors WHERE state = ?");  
         prm = cmd.createParameter("ItemXparm",  
                             AdoEnums.DataType.CHAR,  
                             AdoEnums.ParameterDirection.INPUT,  
                             2,  
                             "CA");  
         cmd.getParameters().append(prm);  
  
         cnConn1.open(strCnn);  
         cmd.setActiveConnection(cnConn1);  
  
         // Connection and CommandType are omitted  
         // because a Command Object is provided.  
         rstAuthors.open(cmd,  
                null ,  
                AdoEnums.CursorType.STATIC,  
                AdoEnums.LockType.READONLY);  
  
         System.out.println(  
            "The Parameters collection accessed by index...");  
         prm = cmd.getParameters().getItem(0);  
         System.out.println("Parameter name = '" +  
                        prm.getName() +  
                        "', value = '" +  
                        prm.getValue().toString() + "'\n");  
  
         System.out.println(  
            "The Parameters collection accessed by name...");  
         prm = cmd.getParameters().getItem("ItemXparm");  
         System.out.println("Parameters name = '" +  
                        prm.getName() +  
                        "', value = '" +  
                        prm.getValue().toString() + "'\n");  
         System.out.println("Press <Enter> to continue..");  
         in.readLine();  
  
         System.out.println(  
            "The Fields collection accessed by index...");  
         rstAuthors.moveFirst();  
         intLimit = rstAuthors.getFields().getCount() - 1;  
         for(intIndex = 0; intIndex <= intLimit; intIndex++)  
         {  
            fld = rstAuthors.getFields().getItem(intIndex);  
            short intVtType = fld.getValue().getvt();  
            String strFieldValue;  
            switch(intVtType)  
            {  
            case Variant.VariantString :  
               strFieldValue = fld.getValue().toString();  
               break;  
            case Variant.VariantBoolean :  
               if(fld.getValue().getBoolean())  
                  strFieldValue = "True";  
               else  
                  strFieldValue = "False";  
               break;  
            default :  
               strFieldValue = fld.getValue().toString();  
               break;  
            }  
            System.out.println("Field " +  
                           Integer.toString(intIndex) +  
                           " : Name = '" +  
                           fld.getName() +  
                           "', value = '" +  
                           strFieldValue +  
                           "'");  
         }  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
  
         System.out.println("The Fields collection accessed by name...");  
         rstAuthors.moveFirst();  
         for(intIndex = 0; intIndex <= 8; intIndex++)  
         {  
            fld = rstAuthors.getFields().getItem  
               (varColumn[intIndex].toString());  
            short intVtType = fld.getValue().getvt();  
            String strFieldValue;  
            switch(intVtType)  
            {  
            case Variant.VariantString :  
               strFieldValue = fld.getValue().toString();  
               break;  
            case Variant.VariantBoolean :  
               if(fld.getValue().getBoolean())  
                  strFieldValue = "True";  
               else  
                  strFieldValue = "False";  
               break;  
            default :  
               strFieldValue = fld.getValue().toString();  
               break;  
            }  
            System.out.println("Field " +  
                           "name = '" +  
                           fld.getName() +  
                           "', value = '" +  
                           strFieldValue +  
                           "'");  
         }  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
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
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();    
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
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
// EndItemJ  
  
```  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)   
 [Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)   
 [Item Property (ADO)](../../../ado/reference/ado-api/item-property-ado.md)   
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)