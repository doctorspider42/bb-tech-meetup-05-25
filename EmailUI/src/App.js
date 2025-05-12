import React from 'react';
import styled from 'styled-components';
import EmailForm from './components/EmailForm';
import { EmailProvider } from './contexts/EmailContext';
import { ThemeProvider } from './contexts/ThemeContext';
import ThemeToggle from './components/ThemeToggle';
import './App.css';

const AppContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  min-height: 100vh;
  padding: 2rem;
  background-color: var(--color-background);
  transition: background-color 0.3s ease;
  
  @media (max-width: 768px) {
    padding: 1rem;
  }
`;

const Header = styled.header`
  margin-bottom: 2rem;
  text-align: center;
`;

const Title = styled.h1`
  color: var(--color-text);
  font-size: 2.5rem;
  margin-bottom: 0.5rem;
  transition: color 0.3s ease;
  
  @media (max-width: 768px) {
    font-size: 2rem;
  }
`;

const Subtitle = styled.p`
  color: var(--color-text-secondary);
  font-size: 1.25rem;
  transition: color 0.3s ease;
`;

const MainContent = styled.main`
  width: 100%;
  max-width: 800px;
  background-color: var(--color-surface);
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  transition: background-color 0.3s ease;
  
  @media (max-width: 768px) {
    padding: 1.5rem;
    border-radius: 8px;
  }
`;

const Footer = styled.footer`
  margin-top: 2rem;
  color: var(--color-text-secondary);
  font-size: 0.875rem;
  transition: color 0.3s ease;
`;

function App() {
  return (
    <ThemeProvider>
      <EmailProvider>
        <AppContainer>
          <ThemeToggle />
          <Header>
            <Title>Email AI Assistant</Title>
            <Subtitle>Get AI-powered insights for your emails</Subtitle>
          </Header>
          <MainContent>
            <EmailForm />
          </MainContent>
          <Footer>
            &copy; {new Date().getFullYear()} Project Ollama
          </Footer>
        </AppContainer>
      </EmailProvider>
    </ThemeProvider>
  );
}

export default App;
