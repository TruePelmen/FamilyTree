namespace FamilyTree.WPF.UserControls
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using FamilyTree.BLL;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Models;

    public partial class Tree : UserControl
    {
        private readonly double halfWidth = 90;
        private int numberOfChildren;
        private double center;
        private int idFocusPerson;
        private int idFocusPersonSpounse;
        private int treeId;
        private string accessType;
        private IPersonService personService;
        private IRelationshipService relationshipService;
        private ITreeService treeService;
        private ITreePersonService treePersonService;


        public Tree(IPersonService personService, IRelationshipService relationshipService, ITreeService treeService, ITreePersonService treePersonService)
        {
            this.relationshipService = relationshipService;
            this.personService = personService;
            this.treePersonService = treePersonService;
            this.InitializeComponent();
            this.childrenPanel.Loaded += this.ChildrenPanelLoaded;
            this.childrenPanel.SizeChanged += this.ChildrenPanelLoaded;
            this.maleFocus.DeletePerson += this.DeletePerson;
            this.femaleFocus.DeletePerson += this.DeletePerson;

            this.maleFocus.PersonAddedFocus += this.FocusPersonAddedHandler;
            this.femaleFocus.PersonAddedFocus += this.FocusPersonAddedHandler;

            this.maleFather.PersonAdded += this.PersonAddedHandler;
            this.maleMother.PersonAdded += this.PersonAddedHandler;
            this.femaleFather.PersonAdded += this.PersonAddedHandler;
            this.femaleMother.PersonAdded += this.PersonAddedHandler;

            this.numberOfChildren = 0;
            this.treeService = treeService;
        }

        public event EventHandler TreeChanged;

        public int TreeId
        {
            get
            {
                return this.treeId;
            }

            set
            {
                this.treeId = value;
                this.FocusPersonId = this.treeService.GetPrimaryPersonId(this.treeId);
            }
        }

        public string AceessType
        {
            set
            {
                this.accessType = value;
                this.CheckAccessType();
            }
        }

        public int FocusPersonId
        {
            get
            {
                return this.idFocusPerson;
            }

            set
            {
                this.idFocusPerson = value;
                this.RedrawTree();
            }
        }

        public int PrimaryPersonId
        {
            get
            {
                return this.treeService.GetPrimaryPersonId(this.treeId);
            }
        }

        public void AddChild(PersonInformation person = null)
        {
            if (this.numberOfChildren != 0 && person != null)
            {
                PersonCard emptyCard = this.childrenPanel.Children.OfType<PersonCard>().FirstOrDefault(card => card.IsEmpty);
                emptyCard.RenewPersonCard(person);
                emptyCard.IdPerson = person.Id;
            }

            PersonCard newChild = new PersonCard();
            newChild.RenewPersonCard(new PersonInformation());
            newChild.Margin = new Thickness(20, 0, 20, 0);
            newChild.Width = 180;
            newChild.Height = 100;
            newChild.MouseLeftButtonDown += this.CardMouseLeftButtonDown;
            newChild.PersonAdded += this.PersonAddedHandler;
            this.childrenPanel.Children.Add(newChild);
            this.numberOfChildren++;
        }

        private void CheckAccessType()
        {
            this.AdjustDisplayFocusCardToAccessType(this.maleFocus);
            this.AdjustDisplayFocusCardToAccessType(this.femaleFocus);
            this.AdjustDisplayPersonCardToAccessType(this.maleFather);
            this.AdjustDisplayPersonCardToAccessType(this.maleMother);
            this.AdjustDisplayPersonCardToAccessType(this.femaleFather);
            this.AdjustDisplayPersonCardToAccessType(this.femaleMother);
            foreach (PersonCard child in this.childrenPanel.Children.OfType<PersonCard>())
            {
                this.AdjustDisplayPersonCardToAccessType(child);
            }
        }

        private void AdjustDisplayFocusCardToAccessType(FocusPersonCard personCard)
        {
            if (this.accessType == "edit")
            {
                if (!personCard.IsEmpty)
                {
                    personCard.deleteButton.Visibility = Visibility.Visible;
                }
                else
                {
                    personCard.emptyPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (!personCard.IsEmpty)
                {
                    personCard.deleteButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    personCard.emptyPanel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void AdjustDisplayPersonCardToAccessType(PersonCard personCard)
        {
            if (this.accessType == "edit")
            {
                if (personCard.IsEmpty)
                {
                    personCard.emptyPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (personCard.IsEmpty)
                {
                    personCard.emptyPanel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ChildrenPanelLoaded(object sender, RoutedEventArgs e)
        {
            this.center = (this.childrenPanel.ActualWidth / 2) - 110;
            this.RedrawLines();
        }

        private void RedrawTree()
        {
            this.childrenPanel.Children.Clear();
            this.numberOfChildren = 0;
            var person = this.personService.GetPersonById(this.idFocusPerson);
            this.idFocusPersonSpounse = this.relationshipService.GetSpouseIdByPersonId(this.idFocusPerson);
            if (person.Gender == "male")
            {
                this.maleFocus.IdPerson = this.idFocusPerson;
                this.maleFocus.RenewPersonCard(person);
                this.maleFather.IdPerson = this.relationshipService.GetFatherIdByPersonId(this.idFocusPerson);
                this.maleFather.RenewPersonCard(this.personService.GetPersonById(this.maleFather.IdPerson));
                this.maleMother.IdPerson = this.relationshipService.GetMotherIdByPersonId(this.idFocusPerson);
                this.maleMother.RenewPersonCard(this.personService.GetPersonById(this.maleMother.IdPerson));
                this.femaleFocus.IdPerson = this.idFocusPersonSpounse;
                this.femaleFocus.RenewPersonCard(this.personService.GetPersonById(this.idFocusPersonSpounse));
                this.femaleFather.IdPerson = this.relationshipService.GetFatherIdByPersonId(this.idFocusPersonSpounse);
                this.femaleFather.RenewPersonCard(this.personService.GetPersonById(this.femaleFather.IdPerson));
                this.femaleMother.IdPerson = this.relationshipService.GetMotherIdByPersonId(this.idFocusPersonSpounse);
                this.femaleMother.RenewPersonCard(this.personService.GetPersonById(this.femaleMother.IdPerson));
            }
            else
            {
                this.femaleFocus.IdPerson = this.idFocusPerson;
                this.femaleFocus.RenewPersonCard(person);
                this.femaleFather.IdPerson = this.relationshipService.GetFatherIdByPersonId(this.idFocusPerson);
                this.femaleFather.RenewPersonCard(this.personService.GetPersonById(this.femaleFather.IdPerson));
                this.femaleMother.IdPerson = this.relationshipService.GetMotherIdByPersonId(this.idFocusPerson);
                this.femaleMother.RenewPersonCard(this.personService.GetPersonById(this.femaleMother.IdPerson));
                this.maleFocus.IdPerson = this.idFocusPersonSpounse;
                this.maleFocus.RenewPersonCard(this.personService.GetPersonById(this.idFocusPersonSpounse));
                this.maleFather.IdPerson = this.relationshipService.GetFatherIdByPersonId(this.idFocusPersonSpounse);
                this.maleFather.RenewPersonCard(this.personService.GetPersonById(this.maleFather.IdPerson));
                this.maleMother.IdPerson = this.relationshipService.GetMotherIdByPersonId(this.idFocusPersonSpounse);
                this.maleMother.RenewPersonCard(this.personService.GetPersonById(this.maleMother.IdPerson));
            }

            var children = this.relationshipService.GetChildrenIdByPersonId(this.idFocusPerson).ToList();
            this.AddChild();
            if (this.idFocusPersonSpounse > 0)
            {
                var spouseChildren = this.relationshipService.GetChildrenIdByPersonId(this.idFocusPersonSpounse).ToList();
                for (int i = 0; i < children.Count; ++i)
                {
                    if (spouseChildren.Contains(children[i]))
                    {
                        this.AddChild(this.personService.GetPersonById(children[i]));
                    }
                }
            }
            else
            {
                for (int i = 0; i < children.Count; ++i)
                {
                    this.AddChild(this.personService.GetPersonById(children[i]));
                }
            }
        }

        private void RedrawLines()
        {
            this.childrenLines.Children.Clear();
            this.DrawVerticalLine(this.center, 0, 35);
            this.DrawMainHorizontalLine();
            if (this.numberOfChildren == 1)
            {
                this.DrawVerticalLine(this.center, 35, 56);
            }
            else
            {
                if (this.numberOfChildren % 2 != 0)
                {
                    this.DrawVerticalLine(this.center, 35, 56);
                    this.DrawOddNumberOfChildren();
                }
                else
                {
                    this.DrawEvenNumberOfChildren();
                }
            }
        }

        private void DrawOddNumberOfChildren()
        {
            double x = 0;
            for (int i = 0; i < this.numberOfChildren / 2; ++i)
            {
                x += 220;
                this.DrawVerticalLine(this.center - x, 35, 56);
                this.DrawVerticalLine(this.center + x, 35, 56);
            }
        }

        private void DrawEvenNumberOfChildren()
        {
            double x = this.halfWidth + 20;
            this.DrawVerticalLine(this.center - x, 35, 56);
            this.DrawVerticalLine(this.center + x, 35, 56);

            for (int i = 1; i < this.numberOfChildren / 2; ++i)
            {
                x += 220;
                this.DrawVerticalLine(this.center - x, 35, 56);
                this.DrawVerticalLine(this.center + x, 35, 56);
            }
        }

        private void DrawMainHorizontalLine()
        {
            double leftBorder = this.center - ((this.halfWidth + 20.0) * (this.numberOfChildren - 1));
            double rightBorder = this.center + ((this.halfWidth + 20.0) * (this.numberOfChildren - 1));
            this.childrenLines.Children.Add(new Line { X1 = leftBorder, Y1 = 35, X2 = rightBorder, Y2 = 35, Stroke = Brushes.Black, StrokeThickness = 1 });
        }

        private void DrawVerticalLine(double x, double y1, double y2)
        {
            this.childrenLines.Children.Add(new Line { X1 = x, Y1 = y1, X2 = x, Y2 = y2, Stroke = Brushes.Black, StrokeThickness = 1 });
        }

        private void CardMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IPersonCard personCard = sender as IPersonCard;
            if (personCard.IdPerson != this.FocusPersonId && !personCard.IsEmpty)
            {
                this.FocusPersonId = personCard.IdPerson;
            }
        }

        private void DeletePerson(object sender, int personId)
        {
            if (personId == this.PrimaryPersonId)
            {
                MessageBox.Show("Неможливо видалити головну особу!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.personService.DeletePerson(personId);
                this.RefocusTree();
                this.TreeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void FocusPersonAddedHandler(object sender, int id)
        {
            this.treePersonService.AddTreePerson(this.TreeId, id);
            PersonInformation person = this.personService.GetPersonById(id);
            if (person.Gender == "male")
            {
                if (!this.maleFather.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.maleFather.IdPerson, person.Id, "father-child");
                }

                if (!this.maleMother.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.maleMother.IdPerson, person.Id, "mother-child");
                }

                if (!this.femaleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.femaleFocus.IdPerson, person.Id, "spouse");
                }

                foreach (PersonCard child in this.childrenPanel.Children.OfType<PersonCard>())
                {
                    if (!child.IsEmpty)
                    {
                        this.relationshipService.AddRelationship(child.IdPerson, person.Id, "father-child");
                    }
                }
            }
            else
            {
                if (!this.femaleFather.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.femaleFather.IdPerson, person.Id, "father-child");
                }

                if (!this.femaleMother.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.femaleMother.IdPerson, person.Id, "mother-child");
                }

                if (!this.maleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(this.maleFocus.IdPerson, person.Id, "spouse");
                }

                foreach (PersonCard child in this.childrenPanel.Children.OfType<PersonCard>())
                {
                    if (!child.IsEmpty)
                    {
                        this.relationshipService.AddRelationship(child.IdPerson, person.Id, "mother-child");
                    }
                }
            }

            this.TreeChanged?.Invoke(this, EventArgs.Empty);
            this.RedrawTree();
        }

        private void PersonAddedHandler(object sender, int id)
        {
            this.treePersonService.AddTreePerson(this.TreeId, id);
            if ((sender as IPersonCard) == this.maleFather)
            {
                if (!this.maleMother.IsEmpty)
                {
                   this.relationshipService.AddRelationship(id, this.maleMother.IdPerson, "spouse");
                }

                if (!this.maleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.maleFocus.IdPerson, "father-child");
                }
            }

            if ((sender as IPersonCard) == this.maleMother)
            {
                if (!this.maleFather.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.maleFather.IdPerson, "spouse");
                }

                if (!this.maleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.maleFocus.IdPerson, "mother-child");
                }

            }

            if ((sender as IPersonCard) == this.femaleFather)
            {
                if (!this.femaleMother.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.femaleMother.IdPerson, "spouse");
                }

                if (!this.femaleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.femaleFocus.IdPerson, "father-child");
                }
            }

            if ((sender as IPersonCard) == this.femaleMother)
            {
                if (!this.femaleFather.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.femaleFather.IdPerson, "spouse");
                }

                if (!this.femaleFocus.IsEmpty)
                {
                    this.relationshipService.AddRelationship(id, this.femaleFocus.IdPerson, "mother-child");
                }
            }

            foreach (PersonCard child in this.childrenPanel.Children.OfType<PersonCard>())
            {
                if ((sender as IPersonCard) == child)
                {
                    if (!this.maleFocus.IsEmpty)
                    {
                        this.relationshipService.AddRelationship(this.maleFocus.IdPerson, id, "father-child");
                    }

                    if (!this.femaleFocus.IsEmpty)
                    {
                        this.relationshipService.AddRelationship(this.femaleFocus.IdPerson, id, "mother-child");
                    }
                }
            }

            this.TreeChanged?.Invoke(this, EventArgs.Empty);
            this.RedrawTree();
        }

        private void RefocusTree()
        {
            if (this.idFocusPersonSpounse > 0)
            {
                this.FocusPersonId = this.idFocusPersonSpounse;
            }
            else
            {
                this.FocusPersonId = this.treeService.GetPrimaryPersonId(this.treeId);
            }
        }
    }
}