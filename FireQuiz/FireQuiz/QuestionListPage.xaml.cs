﻿using System;
using System.Windows;
using System.Windows.Controls;
using FireQuiz.Models;

namespace FireQuiz
{
    public partial class QuestionListPage : Page
    {
        private readonly Quiz quiz;

        public QuestionListPage(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;
            QuestionList.ItemsSource = quiz.Questions;
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Сақтағыңыз келеме?", "Иә",
                                         MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await FirebaseHelper.SaveQuiz(quiz);
                MessageBox.Show("Тест базаға сақталды!");
                NavigationService.Navigate(new SubjectsPage());

            }
            else if (result == MessageBoxResult.No)
            {
                NavigationService.Navigate(new SubjectsPage());
            }
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateQuestionPage(quiz));
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionList.SelectedItem is Question selectedQuestion)
                NavigationService.Navigate(new CreateQuestionPage(quiz, selectedQuestion));
            else
                MessageBox.Show("Өзгерту үшін сұрақты таңдаңыз!");
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionList.SelectedItem is Question selectedQuestion)
            {
                quiz.Questions.Remove(selectedQuestion);
                QuestionList.Items.Refresh();
            }
            else
                MessageBox.Show("Өшіру үшін сұрақты таңдаңыз!");
        }

        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await FirebaseHelper.SaveQuiz(quiz);
                MessageBox.Show("Тест базаға сақталды!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении теста: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
