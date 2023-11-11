namespace FamilyTree.WPF.UserControls
{
    /// <summary>
    /// An interface that defines properties for a person card.
    /// </summary>
    public interface IPersonCard
    {
        /// <summary>
        /// Gets or sets the unique identifier of a person.
        /// </summary>
        int IdPerson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person card is empty.
        /// </summary>
        bool IsEmpty { get; set; }
    }
}