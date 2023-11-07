// <copyright file="ITreeService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing family trees.
    /// </summary>
    public interface ITreeService
    {
        /// <summary>
        /// Retrieves all family trees in the system.
        /// </summary>
        /// <returns>An IEnumerable of Tree objects representing all family trees.</returns>
        IEnumerable<Tree> GetAllTrees();

        /// <summary>
        /// Retrieves a family tree by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the family tree.</param>
        /// <returns>The Tree object with the specified identifier, or null if not found.</returns>
        Tree GetTreeById(int id);

        /// <summary>
        /// Retrieves the unique identifier of the primary person associated with a family tree.
        /// </summary>
        /// <param name="treeId">The unique identifier of the family tree.</param>
        /// <returns>The unique identifier of the primary person in the family tree, or -1 if not found.</returns>
        int GetPrimaryPersonId(int treeId);

        /// <summary>
        /// Adds a new family tree to the system.
        /// </summary>
        /// <param name="name">The name of the family tree.</param>
        /// <returns>The unique identifier of the newly added family tree.</returns>
        int AddTree(string name);

        /// <summary>
        /// Updates an existing family tree in the system.
        /// </summary>
        /// <param name="id">The unique identifier of the family tree to update.</param>
        /// <param name="name">The updated name of the family tree.</param>
        void UpdateTree(int id, string name);

        /// <summary>
        /// Deletes a family tree from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the family tree to delete.</param>
        void DeleteTree(int id);
    }
}