import React, { useEffect } from 'react';
import styled from 'styled-components';
import { useEmail } from '../contexts/EmailContext';
import { useTheme } from '../contexts/ThemeContext';
import LoadingSkeleton from './LoadingSkeleton';
import PresetTabs from './PresetTabs';

const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: var(--spacing-lg, 1.5rem);
`;

const TextareaContainer = styled.div`
  position: relative;
`;

const Label = styled.label`
  display: block;
  margin-bottom: var(--spacing-sm, 0.5rem);
  font-weight: 500;
  color: var(--color-text);
`;

const Textarea = styled.textarea`
  width: 100%;
  min-height: 200px;
  padding: var(--spacing-md, 1rem);
  border: 1px solid ${props => props.error ? 'var(--color-error, #e53e3e)' : 'var(--color-border)'};
  border-radius: var(--border-radius-medium, 8px);
  background-color: var(--color-surface);
  color: var(--color-text);
  transition: background-color 0.3s ease, color 0.3s ease, border-color 0.3s ease;  font-family: inherit;
  font-size: var(--font-size-md, 1rem);
  line-height: 1.5;
  resize: vertical;
  transition: border-color 0.2s, box-shadow 0.2s;
  
  &:focus {
    outline: none;
    border-color: var(--color-primary, #4299e1);
    box-shadow: 0 0 0 3px rgba(66, 153, 225, 0.2);
  }
`;

const CharCount = styled.div`
  position: absolute;
  bottom: 0.5rem;
  right: 0.5rem;
  font-size: 0.75rem;
  color: ${props => props.isNearLimit ? 'var(--color-error)' : 'var(--color-textLight)'};
`;

const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  background-color: var(--color-primary);
  color: white;
  font-weight: 600;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  transition: background-color 0.2s, transform 0.1s;
  align-self: flex-end;
  
  &:hover {
    background-color: var(--color-primaryDark);
  }
  
  &:active {
    transform: translateY(1px);
  }
  
  &:disabled {
    background-color: var(--color-textLight);
    cursor: not-allowed;
  }
  
  @media (max-width: 768px) {
    width: 100%;
    padding: 0.75rem 1rem;
  }
`;

const ResponseContainer = styled.div`
  margin-top: 2rem;
  padding: 1.5rem;
  background-color: ${props => props.theme.currentTheme === 'dark' ? 'rgba(255, 255, 255, 0.05)' : 'var(--color-surface)'};
  border-radius: 8px;
  border-left: 4px solid var(--color-primary);
`;

const ResponseTitle = styled.h3`
  color: var(--color-primary);
  margin-bottom: 0.75rem;
  font-weight: 600;
`;

const ResponseContent = styled.div`
  white-space: pre-wrap;
  line-height: 1.6;
  color: var(--color-resultText);
`;

const ErrorMessage = styled.div`
  color: var(--color-error);
  padding: 1rem;
  background-color: ${props => props.theme.name === 'dark' ? 'rgba(229, 62, 62, 0.1)' : '#fff5f5'};
  border-radius: 8px;
  margin-top: 1rem;
`;

const LoadingSpinner = styled.div`
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-top: 2px solid white;
  border-radius: 50%;
  width: 1rem;
  height: 1rem;
  animation: spin 1s linear infinite;
  
  @keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
  }
`;

const StyledPre = styled.pre`
  background-color: ${props => props.theme.currentTheme === 'dark' ? 'rgba(0, 0, 0, 0.2)' : 'rgba(0, 0, 0, 0.05)'};
  padding: 1rem;
  border-radius: var(--border-radius-small);
  overflow-x: auto;
  color: var(--color-resultText);
  font-family: 'Courier New', monospace;
`;

const EmailForm = () => {
  const { 
    emailContent, 
    setEmailContent, 
    response, 
    loading, 
    error, 
    processEmail
  } = useEmail();
  
  const { currentTheme } = useTheme();
  
  const maxLength = 5000;

  // Add effect to log when response changes
  useEffect(() => {
    if (response) {
      console.log('Response received in EmailForm:', response);
    }
  }, [response]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!emailContent.trim()) return;
    
    // Process the email directly instead of showing confirmation dialog
    processEmail();
  };
  
  return (
    <>      <Form onSubmit={handleSubmit}>
        <PresetTabs />
        <TextareaContainer>
          <Label htmlFor="emailContent">Email content (edit as needed):</Label>
          <Textarea
            id="emailContent"
            value={emailContent}
            onChange={(e) => setEmailContent(e.target.value)}
            placeholder="Enter your email here or select a template above..."
            maxLength={maxLength}
            error={!!error}
            required
          />
          <CharCount isNearLimit={emailContent.length > maxLength * 0.9}>
            {emailContent.length}/{maxLength}
          </CharCount>
        </TextareaContainer>
          <Button type="submit" disabled={loading || !emailContent.trim()}>
          {loading ? <LoadingSpinner /> : null}
          {loading ? 'Processing...' : 'Analyze Email'}
        </Button>
          {error && <ErrorMessage theme={{ name: currentTheme }}>{error}</ErrorMessage>}
        
        {loading && <LoadingSkeleton />}        {response && !loading && (
          <ResponseContainer theme={{ currentTheme }}>
            <ResponseTitle>AI Analysis</ResponseTitle>            <ResponseContent>              {response.tool && <div><strong>Tool:</strong> {response.tool}</div>}
              {response.details && <div><strong>Details:</strong><br/>{response.details}</div>}
              {response.justification && <div><strong>Justification:</strong><br/>{response.justification}</div>}
              {response.totalTime && <div><strong>Total Time:</strong> {response.totalTime}</div>}
              {!response.tool && !response.details && !response.justification && typeof response === 'object' && (
                <StyledPre theme={{ currentTheme }}>{JSON.stringify(response, null, 2)}</StyledPre>
              )}
              {typeof response === 'string' && response}
            </ResponseContent>
          </ResponseContainer>
        )}
      </Form>
    </>
  );
};

export default EmailForm;
