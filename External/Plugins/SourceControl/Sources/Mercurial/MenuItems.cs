﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PluginCore;
using PluginCore.Localization;
using ProjectManager.Controls.TreeView;

namespace SourceControl.Sources.Mercurial
{
    class MenuItems : IVCMenuItems
    {
        TreeNode[] currentNodes;
        IVCManager currentManager;

        readonly ToolStripItem update;
        readonly ToolStripItem commit;
        readonly ToolStripItem push;
        readonly ToolStripItem showLog;
        readonly ToolStripItem midSeparator;
        readonly ToolStripItem annotate;
        readonly ToolStripItem diffChange;
        readonly ToolStripItem add;
        readonly ToolStripItem ignore;
        readonly ToolStripItem undoAdd;
        readonly ToolStripItem revert;
        readonly ToolStripItem editConflict;

        public TreeNode[] CurrentNodes { set => currentNodes = value; }
        public IVCManager CurrentManager { set => currentManager = value; }

        public ToolStripItem Update => update;
        public ToolStripItem Commit => commit;
        public ToolStripItem Push => push;
        public ToolStripItem ShowLog => showLog;
        public ToolStripItem MidSeparator => midSeparator;
        public ToolStripItem Annotate => annotate;
        public ToolStripItem Diff => null;
        public ToolStripItem DiffChange => diffChange;
        public ToolStripItem Add => add;
        public ToolStripItem Ignore => ignore;
        public ToolStripItem UndoAdd => undoAdd;
        public ToolStripItem Revert => revert;
        public ToolStripItem EditConflict => editConflict;

        private readonly Dictionary<ToolStripItem, VCMenuItemProperties> items = new Dictionary<ToolStripItem, VCMenuItemProperties>();
        public Dictionary<ToolStripItem, VCMenuItemProperties> Items => items;

        public MenuItems()
        {
            update = new ToolStripMenuItem(TextHelper.GetString("Label.Pull"), PluginBase.MainForm.FindImage("159|1|-3|3"), Update_Click);
            commit = new ToolStripMenuItem(TextHelper.GetString("Label.Commit"), PluginBase.MainForm.FindImage("62"), Commit_Click);
            push = new ToolStripMenuItem(TextHelper.GetString("Label.Push"), PluginBase.MainForm.FindImage("159|9|-3|3"), Push_Click);
            showLog = new ToolStripMenuItem(TextHelper.GetString("Label.ShowLog"), PluginBase.MainForm.FindImage("95"), ShowLog_Click);
            midSeparator = new ToolStripSeparator();
            annotate = new ToolStripMenuItem("Annotate", PluginBase.MainForm.FindImage("45")) { Enabled = false };
            diffChange = new ToolStripMenuItem(TextHelper.GetString("Label.DiffWithPrevious"), PluginBase.MainForm.FindImage("251"), DiffChange_Click);
            add = new ToolStripMenuItem(TextHelper.GetString("Label.Add"), PluginBase.MainForm.FindImage("33"), Add_Click);
            ignore = new ToolStripMenuItem(TextHelper.GetString("Label.AddToIgnoreList"), PluginBase.MainForm.FindImage("166"), Ignore_Click);
            undoAdd = new ToolStripMenuItem(TextHelper.GetString("Label.UndoAdd"), PluginBase.MainForm.FindImage("73"), UndoAdd_Click);
            revert = new ToolStripMenuItem(TextHelper.GetString("Label.Revert"), PluginBase.MainForm.FindImage("73"), Revert_Click);
            editConflict = new ToolStripMenuItem(TextHelper.GetString("Label.EditConflict"), PluginBase.MainForm.FindImage("196"), EditConflict_Click);
        }

        private string GetPaths()
        {
            return string.Join("*", GetPathsArray());
        }

        private string[] GetPathsArray()
        {
            List<string> paths = new List<string>();
            if (currentNodes != null)
                foreach (TreeNode node in currentNodes)
                    if (node is GenericNode) paths.Add((node as GenericNode).BackingPath);

            return paths.ToArray();
        }

        void EditConflict_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("merge", GetPaths());
        }

        void Revert_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("revert", GetPaths());
        }

        void UndoAdd_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("forget", GetPaths());
        }

        void Add_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("add", GetPaths());
        }

        void Ignore_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("ignore", GetPaths());
        }

        void DiffChange_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("vdiff", GetPaths());
        }

        void ShowLog_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("log", GetPaths());
        }

        void Push_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("push", GetPaths());
        }

        void Commit_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("commit", GetPaths());
        }

        void Update_Click(object sender, EventArgs e)
        {
            TortoiseProc.Execute("pull", GetPaths());
        }
    }
}
