﻿using System;
using System.IO;
using System.Net.Http;

namespace WebDav
{
    /// <summary>
    /// Represents a response of the GET operation.
    /// The class has to be properly disposed.
    /// </summary>
    public class WebDavStreamResponse : WebDavResponse, IDisposable
    {
        private readonly HttpResponseMessage _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDavStreamResponse"/> class.
        /// </summary>
        /// <param name="response">The raw http response.</param>
        /// <param name="stream">The stream of content of the resource.</param>
        public WebDavStreamResponse(HttpResponseMessage response, Stream stream)
            : base((int)response.StatusCode, response.ReasonPhrase)
        {
            _response = response;
            Stream = stream;
        }

        /// <summary>
        /// Gets the stream of content of the resource.
        /// </summary>
        public Stream Stream { get; }

        public long? ContentLength
        {
            get
            {
                return _response?.Content?.Headers?.ContentLength;
            }
        }

        public override string ToString()
        {
            return $"WebDAV stream response - StatusCode: {StatusCode}, Description: {Description}";
        }

        public void Dispose()
        {
            Stream?.Dispose();
            _response?.Dispose();
        }
    }
}
