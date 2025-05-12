import React from 'react';
import styled from 'styled-components';

const OverlayContainer = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
`;

const DialogContainer = styled.div`
  background-color: var(--color-surface);
  border-radius: var(--border-radius-large, 12px);
  padding: var(--spacing-xl, 2rem);
  width: 90%;
  max-width: 500px;
  box-shadow: var(--shadow-large);
  color: var(--color-text);
  transition: background-color 0.3s ease, color 0.3s ease;
  animation: slideIn 0.3s ease-out forwards;
  
  @keyframes slideIn {
    from {
      transform: translateY(30px);
      opacity: 0;
    }
    to {
      transform: translateY(0);
      opacity: 1;
    }
  }
`;

const DialogTitle = styled.h3`
  font-size: var(--font-size-xl, 1.5rem);
  margin-bottom: var(--spacing-md, 1rem);
  color: var(--color-text);
  transition: color 0.3s ease;
`;

const DialogContent = styled.div`
  margin-bottom: var(--spacing-lg, 1.5rem);
  color: var(--color-text-secondary);
  line-height: 1.6;
  transition: color 0.3s ease;
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: var(--spacing-md, 1rem);
`;

const Button = styled.button`
  background-color: ${props => props.primary ? 'var(--color-primary)' : 'transparent'};
  color: ${props => props.primary ? 'white' : 'var(--color-primary)'};
  border: ${props => props.primary ? 'none' : '1px solid var(--color-primary)'};
  transition: background-color 0.3s ease, color 0.3s ease, border-color 0.3s ease;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  
  &:hover {
    background-color: ${props => props.primary ? '#3182ce' : 'rgba(66, 153, 225, 0.1)'};
  }
`;

const ConfirmationDialog = ({ title, message, onClose, onConfirm }) => {
  const handleConfirm = () => {
    onConfirm();
    // Make sure dialog closes after confirming
    onClose();
  };

  return (
    <OverlayContainer>
      <DialogContainer>
        <DialogTitle>{title}</DialogTitle>
        <DialogContent>{message}</DialogContent>
        <ButtonContainer>
          <Button onClick={onClose}>Cancel</Button>
          <Button primary onClick={handleConfirm}>Continue</Button>
        </ButtonContainer>
      </DialogContainer>
    </OverlayContainer>
  );
};

export default ConfirmationDialog;
