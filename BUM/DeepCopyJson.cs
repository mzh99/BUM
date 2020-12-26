using System;
using System.Text.Json;

namespace BUM {

   public static class SystemExtension {

      public static T DeepCopyUsingJson<T>(this T source) {
         var data = JsonSerializer.SerializeToUtf8Bytes<T>(source);
         var readOnlySpan = new ReadOnlySpan<byte>(data);
         return JsonSerializer.Deserialize<T>(readOnlySpan);
      }

   }

}
