// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ManagedCommon;
using Microsoft.PowerToys.Settings.UI.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Wox.Infrastructure;
using Wox.Plugin;
using Wox.Plugin.Logger;

namespace Community.PowerToys.Run.Plugin.Test
{
    public class Main : IPlugin, IPluginI18n, IReloadable, IDisposable
    {
        private PluginInitContext _context;

        private string _iconPath;
        private string _iconPath1;
        private string _iconPath2;

        private bool _disposed;

        public string Name => "Test";

        public string Description => "Test plugin";

        // TODO: remove dash then change to uppercase from ID below and inside plugin.json
        public static string PluginID => "A01CAA00226A455CBA2ABD0C10D1F685";

        // TODO: return query results
        public List<Result> Query(Query query)
        {
            ArgumentNullException.ThrowIfNull(query);

            return [
                new Result
                {
                    Title = "Test",
                    SubTitle = "Test subtitle",
                    IcoPath = _iconPath1,
                    Action = _ =>
                    {
                        _context.API.ShowMsg("Test", "Test subtitle");
                        return true;
                    }
                },
                new Result
                {
                    Title = "Test1",
                    SubTitle = "Test1 subtitle",
                    IcoPath = _iconPath2,
                    Action = _ =>
                    {
                        _context.API.ShowMsg("Test1", "Test1 subtitle");
                        return true;
                    }
                }
            ];
        }

        public void Init(PluginInitContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(_context.API.GetCurrentTheme());
        }

        public string GetTranslatedPluginTitle()
        {
            return Name;
        }

        public string GetTranslatedPluginDescription()
        {
            return Description;
        }

        private void OnThemeChanged(Theme oldtheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }

        private void UpdateIconPath(Theme theme)
        {
            if (theme == Theme.Light || theme == Theme.HighContrastWhite)
            {
                _iconPath = "Images/Test.light.png";
                _iconPath1 = "Images/Icon.light.png";
                _iconPath2 = "Images/Icon2.light.png";
            }
            else
            {
                _iconPath = "Images/Test.dark.png";
                _iconPath1 = "Images/Icon.dark.png";
                _iconPath2 = "Images/Icon2.dark.png";
            }
        }

        public Control CreateSettingPanel()
        {
            throw new NotImplementedException();
        }

        public void ReloadData()
        {
            if (_context is null)
            {
                return;
            }

            UpdateIconPath(_context.API.GetCurrentTheme());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_context != null && _context.API != null)
                {
                    _context.API.ThemeChanged -= OnThemeChanged;
                }

                _disposed = true;
            }
        }
    }
}
