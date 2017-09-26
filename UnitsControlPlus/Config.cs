﻿using System;

using Ensage;
using Ensage.Common.Menu;
using Ensage.SDK.Menu;

using SharpDX;

namespace UnitsControlPlus
{
    internal class Config : IDisposable
    {
        private MenuFactory Factory { get; }

        public UnitsControlPlus UnitsControlPlus { get; }

        public Vector2 Screen { get; }

        public MenuItem<bool> TextItem { get; }

        public MenuItem<Slider> TextXItem { get; }

        public MenuItem<Slider> TextYItem { get; }

        public MenuItem<Slider> RadiusTargetUnitsItem { get; }

        public MenuItem<KeyBind> PressKeyItem { get; }

        public MenuItem<KeyBind> ToggleKeyItem { get; }

        public MenuItem<KeyBind> ChangeTargetItem { get; }

        public MenuItem<StringList> PressTargetItem { get; }

        public MenuItem<Slider> ControlWithoutTargetItem { get; }

        public MenuItem<StringList> WithoutTargetItem { get; }

        public Mode Mode { get; }

        public UpdateMode UpdateMode { get; }

        private Renderer Renderer { get; }

        private bool Disposed { get; set; }

        public Config(UnitsControlPlus unitscontrolplus)
        {
            UnitsControlPlus = unitscontrolplus;
            Screen = new Vector2(Drawing.Width - 160, Drawing.Height);

            Factory = MenuFactory.CreateWithTexture("UnitsControlPlus", "unitscontrolplus");
            Factory.Target.SetFontColor(Color.Aqua);

            var DrawingMenu = Factory.Menu("Drawing");
            TextItem = DrawingMenu.Item("Text", true);
            TextXItem = DrawingMenu.Item("X", new Slider(0, 0, (int)Screen.X - 60));
            TextYItem = DrawingMenu.Item("Y", new Slider(0, 0, (int)Screen.Y - 200));

            RadiusTargetUnitsItem = Factory.Item("Radius from Target to Units", new Slider(5000, 0, 10000));
            RadiusTargetUnitsItem.Item.SetTooltip("Activation for Units from Target Radius");
            PressKeyItem = Factory.Item("Press Key", new KeyBind('0'));
            PressTargetItem = Factory.Item("Press Target", new StringList("Lock", "Default"));
            ToggleKeyItem = Factory.Item("Toggle Key", new KeyBind('0', KeyBindType.Toggle, false));
            ChangeTargetItem = Factory.Item("Change Target Key", new KeyBind('0'));

            ControlWithoutTargetItem = Factory.Item("Control Without Target in Radius", new Slider(5000, 0, 10000));
            ControlWithoutTargetItem.Item.SetTooltip("Control all Units in this radius, if there is no Target");
            WithoutTargetItem = Factory.Item("Without Target", new StringList("Move Mouse Position", "Follow on Hero", "None"));

            Mode = new Mode(this);
            UpdateMode = new UpdateMode(this);
            Renderer = new Renderer(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                Renderer.Dispose();
                UpdateMode.Dispose();
                Mode.Dispose();
                Factory.Dispose();
            }

            Disposed = true;
        }
    }
}
