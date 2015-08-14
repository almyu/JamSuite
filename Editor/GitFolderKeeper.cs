//#define DEBUG_KEEPER

using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace JamSuite.Editor {

    public class GitFolderKeeper : AssetPostprocessor {

        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            var cache = new HashSet<string>();

            foreach (var deleted in deletedAssets)
                KeepEmptyFolders(deleted, true, cache);

            foreach (var moved in movedFromAssetPaths)
                KeepEmptyFolders(moved, true, cache);

            foreach (var moved in movedAssets)
                KeepEmptyFolders(moved, true, cache);

            foreach (var imported in importedAssets)
                KeepEmptyFolders(imported, true, cache);
        }


        public static void KeepEmptyFolders(string path, bool recurse = true, HashSet<string> cache = null) {
            if (string.IsNullOrEmpty(path)) return; // happens on rename

            var dir = Directory.Exists(path) ? path : Path.GetDirectoryName(path);

            for (; dir != ""; dir = Path.GetDirectoryName(dir)) {
                if (cache != null && cache.Contains(dir)) continue;
                if (!Directory.Exists(dir)) continue; // happens in edgy import situations

                var keeperPath = Path.Combine(dir, ".keep");

                var kept = File.Exists(keeperPath);
                var empty = Directory.GetFiles(dir).Length == (kept ? 1 : 0);

                if (empty != kept) {
#if DEBUG_KEEPER
                    if (empty) {
                        File.CreateText(keeperPath).Dispose();
                        Debug.Log("Keeper created in " + dir);
                    }
                    else {
                        File.Delete(keeperPath);
                        Debug.Log("Keeper deleted from " + dir);
                    }
#else
                    if (empty) File.CreateText(keeperPath).Dispose();
                    else File.Delete(keeperPath);
#endif
                }

                if (cache != null) cache.Add(dir);

                if (!recurse) break;
            }
        }
    }
}
