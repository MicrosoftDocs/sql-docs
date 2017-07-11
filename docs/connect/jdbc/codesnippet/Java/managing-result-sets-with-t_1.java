public static void executeStatement(Connection con) {
   try {
      String SQL = "SELECT TOP 10 * FROM Person.Contact";
      Statement stmt = con.createStatement();
      ResultSet rs = stmt.executeQuery(SQL);

      while (rs.next()) {
         System.out.println(rs.getString(4) + " " + rs.getString(6));
      }
      rs.close();
      stmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}