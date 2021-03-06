﻿namespace Statiq.Common
{
    public partial interface IExecutionContext
    {
        /// <summary>
        /// Gets a link for the root of the site using the host and root path specified in the settings.
        /// </summary>
        /// <returns>A link for the root of the site.</returns>
        public string GetLink() =>
            GetLink(
                (NormalizedPath)null,
                Settings.GetString(Common.Keys.Host),
                Settings.GetDirectoryPath(Common.Keys.LinkRoot),
                Settings.GetBool(Common.Keys.LinksUseHttps),
                false,
                false);

        /// <summary>
        /// Gets a link for the specified document using the document destination.
        /// This version should be used inside modules to ensure
        /// consistent link generation. Note that you can optionally include the host or not depending
        /// on if you want to generate host-specific links. By default, the host is not included so that
        /// sites work the same on any server including the preview server.
        /// </summary>
        /// <param name="document">The document to generate a link for.</param>
        /// <param name="includeHost">
        /// If set to <c>true</c> the host configured in the output settings will
        /// be included in the link, otherwise the host will be omitted and only the root path will be included (default).
        /// </param>
        /// <returns>
        /// A string representation of the path suitable for a web link.
        /// </returns>
        public string GetLink(IDocument document, bool includeHost = false) =>
            document.Destination == null ? null : GetLink(document.Destination, includeHost);

        /// <summary>
        /// Gets a link for the specified metadata using the specified metadata value and the default settings from the
        /// configuration. This version should be used inside modules to ensure
        /// consistent link generation. Note that you can optionally include the host or not depending
        /// on if you want to generate host-specific links. By default, the host is not included so that
        /// sites work the same on any server including the preview server.
        /// </summary>
        /// <param name="metadata">The metadata or document to generate a link for.</param>
        /// <param name="key">The key at which a <see cref="FilePath"/> can be found for generating the link.</param>
        /// <param name="includeHost">
        /// If set to <c>true</c> the host configured in the output settings will
        /// be included in the link, otherwise the host will be omitted and only the root path will be included (default).
        /// </param>
        /// <returns>
        /// A string representation of the path suitable for a web link.
        /// </returns>
        public string GetLink(IMetadata metadata, string key, bool includeHost = false)
        {
            if (metadata?.ContainsKey(key) == true)
            {
                // Return the actual URI if it's absolute
                if (LinkGenerator.TryGetAbsoluteHttpUri(metadata.GetString(key), out string absoluteUri))
                {
                    return absoluteUri;
                }

                // Otherwise try to process the value as a file path
                FilePath filePath = metadata.GetFilePath(key);
                return filePath != null ? GetLink(filePath, includeHost) : null;
            }
            return null;
        }

        /// <summary>
        /// Converts the specified path into a string appropriate for use as a link using default settings from the
        /// configuration. This version should be used inside modules to ensure
        /// consistent link generation. Note that you can optionally include the host or not depending
        /// on if you want to generate host-specific links. By default, the host is not included so that
        /// sites work the same on any server including the preview server.
        /// </summary>
        /// <param name="path">The path to generate a link for.</param>
        /// <param name="includeHost">If set to <c>true</c> the host configured in the output settings will
        /// be included in the link, otherwise the host will be omitted and only the root path will be included (default).</param>
        /// <returns>
        /// A string representation of the path suitable for a web link.
        /// </returns>
        public string GetLink(string path, bool includeHost = false)
        {
            // Return the actual URI if it's absolute
            if (path != null && LinkGenerator.TryGetAbsoluteHttpUri(path, out string absoluteUri))
            {
                return absoluteUri;
            }

            // Otherwise process the path as a FilePath
            return GetLink(
                path == null ? null : new FilePath(path),
                includeHost ? Settings.GetString(Common.Keys.Host) : null,
                Settings.GetDirectoryPath(Common.Keys.LinkRoot),
                Settings.GetBool(Common.Keys.LinksUseHttps),
                Settings.GetBool(Common.Keys.LinkHideIndexPages),
                Settings.GetBool(Common.Keys.LinkHideExtensions),
                Settings.GetBool(Common.Keys.LinkLowercase));
        }

        /// <summary>
        /// Converts the path into a string appropriate for use as a link, overriding one or more
        /// settings from the configuration.
        /// </summary>
        /// <param name="path">The path to generate a link for.</param>
        /// <param name="host">The host to use for the link.</param>
        /// <param name="root">The root of the link. The value of this parameter is prepended to the path.</param>
        /// <param name="useHttps">If set to <c>true</c>, HTTPS will be used as the scheme for the link.</param>
        /// <param name="hideIndexPages">If set to <c>true</c>, "index.htm" and "index.html" file
        /// names will be hidden.</param>
        /// <param name="hideExtensions">If set to <c>true</c>, extensions will be hidden.</param>
        /// <returns>
        /// A string representation of the path suitable for a web link with the specified
        /// root and hidden file name or extension.
        /// </returns>
        public string GetLink(string path, string host, DirectoryPath root, bool useHttps, bool hideIndexPages, bool hideExtensions)
        {
            // Return the actual URI if it's absolute
            if (path != null && LinkGenerator.TryGetAbsoluteHttpUri(path, out string absoluteUri))
            {
                return absoluteUri;
            }

            // Otherwise process the path as a FilePath
            return GetLink(path == null ? null : new FilePath(path), host, root, useHttps, hideIndexPages, hideExtensions);
        }

        /// <summary>
        /// Converts the specified path into a string appropriate for use as a link using default settings from the
        /// configuration. This version should be used inside modules to ensure
        /// consistent link generation. Note that you can optionally include the host or not depending
        /// on if you want to generate host-specific links. By default, the host is not included so that
        /// sites work the same on any server including the preview server.
        /// </summary>
        /// <param name="path">The path to generate a link for.</param>
        /// <param name="includeHost">If set to <c>true</c> the host configured in the output settings will
        /// be included in the link, otherwise the host will be omitted and only the root path will be included (default).</param>
        /// <returns>
        /// A string representation of the path suitable for a web link.
        /// </returns>
        public string GetLink(NormalizedPath path, bool includeHost = false) =>
            GetLink(
                path,
                includeHost ? Settings.GetString(Common.Keys.Host) : null,
                Settings.GetDirectoryPath(Common.Keys.LinkRoot),
                Settings.GetBool(Common.Keys.LinksUseHttps),
                Settings.GetBool(Common.Keys.LinkHideIndexPages),
                Settings.GetBool(Common.Keys.LinkHideExtensions),
                Settings.GetBool(Common.Keys.LinkLowercase));

        /// <summary>
        /// Converts the path into a string appropriate for use as a link, overriding one or more
        /// settings from the configuration.
        /// </summary>
        /// <param name="path">The path to generate a link for.</param>
        /// <param name="host">The host to use for the link.</param>
        /// <param name="root">The root of the link. The value of this parameter is prepended to the path.</param>
        /// <param name="useHttps">If set to <c>true</c>, HTTPS will be used as the scheme for the link.</param>
        /// <param name="hideIndexPages">If set to <c>true</c>, "index.htm" and "index.html" file
        /// names will be hidden.</param>
        /// <param name="hideExtensions">If set to <c>true</c>, extensions will be hidden.</param>
        /// <returns>
        /// A string representation of the path suitable for a web link with the specified
        /// root and hidden file name or extension.
        /// </returns>
        public string GetLink(
            NormalizedPath path,
            string host,
            DirectoryPath root,
            bool useHttps,
            bool hideIndexPages,
            bool hideExtensions) =>
            GetLink(
                path,
                host,
                root,
                useHttps,
                hideIndexPages,
                hideExtensions,
                Settings.GetBool(Common.Keys.LinkLowercase));

        /// <summary>
        /// Converts the path into a string appropriate for use as a link, overriding one or more
        /// settings from the configuration.
        /// </summary>
        /// <param name="path">The path to generate a link for.</param>
        /// <param name="host">The host to use for the link.</param>
        /// <param name="root">The root of the link. The value of this parameter is prepended to the path.</param>
        /// <param name="useHttps">If set to <c>true</c>, HTTPS will be used as the scheme for the link.</param>
        /// <param name="hideIndexPages">If set to <c>true</c>, "index.htm" and "index.html" file
        /// names will be hidden.</param>
        /// <param name="hideExtensions">If set to <c>true</c>, extensions will be hidden.</param>
        /// <param name="lowercase">If set to <c>true</c>, links will be rendered in all lowercase.</param>
        /// <returns>
        /// A string representation of the path suitable for a web link with the specified
        /// root and hidden file name or extension.
        /// </returns>
        public string GetLink(
            NormalizedPath path,
            string host,
            DirectoryPath root,
            bool useHttps,
            bool hideIndexPages,
            bool hideExtensions,
            bool lowercase) =>
            LinkGenerator.GetLink(
                path,
                host,
                root,
                useHttps ? "https" : null,
                hideIndexPages ? LinkGenerator.DefaultHidePages : null,
                hideExtensions ? LinkGenerator.DefaultHideExtensions : null,
                lowercase);
    }
}
